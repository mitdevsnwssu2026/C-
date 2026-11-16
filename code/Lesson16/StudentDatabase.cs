// StudentDatabase.cs - every database command for students lives here

using Microsoft.Data.Sqlite;

public static class StudentDatabase
{
    private const string ConnectionString = "Data Source=school.db";

    // Opens a connection to school.db and gives it back, ready to use
    private static SqliteConnection OpenConnection()
    {
        SqliteConnection connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    // CREATE: make the table if it does not exist yet
    public static void CreateTable()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Students (
                Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentNumber TEXT    NOT NULL UNIQUE,
                FullName      TEXT    NOT NULL,
                Course        TEXT    NOT NULL,
                YearLevel     INTEGER NOT NULL
            );";
        command.ExecuteNonQuery();
    }

    // Is this student number already used?
    public static bool StudentNumberExists(string studentNumber)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Students WHERE StudentNumber = $studentNumber;";
        command.Parameters.AddWithValue("$studentNumber", studentNumber);

        int count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    // CREATE: save a new student
    public static void AddStudent(Student student)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Students (StudentNumber, FullName, Course, YearLevel)
            VALUES ($studentNumber, $fullName, $course, $yearLevel);";
        command.Parameters.AddWithValue("$studentNumber", student.StudentNumber);
        command.Parameters.AddWithValue("$fullName", student.FullName);
        command.Parameters.AddWithValue("$course", student.Course);
        command.Parameters.AddWithValue("$yearLevel", student.YearLevel);
        command.ExecuteNonQuery();
    }

    // READ: get every student, sorted by name
    public static List<Student> GetAllStudents()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            ORDER BY FullName;";

        return ReadStudents(command);
    }

    // READ: get students whose name contains the keyword
    public static List<Student> SearchByName(string keyword)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            WHERE FullName LIKE $pattern
            ORDER BY FullName;";
        command.Parameters.AddWithValue("$pattern", "%" + keyword + "%");

        return ReadStudents(command);
    }

    // READ: find one student by student number (gives back null if not found)
    public static Student? FindByStudentNumber(string studentNumber)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            WHERE StudentNumber = $studentNumber;";
        command.Parameters.AddWithValue("$studentNumber", studentNumber);

        List<Student> results = ReadStudents(command);

        if (results.Count == 0)
        {
            return null;
        }

        return results[0];
    }

    // UPDATE: save new values for an existing student
    public static bool UpdateStudent(Student student)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Students
            SET FullName = $fullName,
                Course = $course,
                YearLevel = $yearLevel
            WHERE Id = $id;";
        command.Parameters.AddWithValue("$fullName", student.FullName);
        command.Parameters.AddWithValue("$course", student.Course);
        command.Parameters.AddWithValue("$yearLevel", student.YearLevel);
        command.Parameters.AddWithValue("$id", student.Id);

        int rowsChanged = command.ExecuteNonQuery();
        return rowsChanged > 0;
    }

    // DELETE: remove a student
    public static bool DeleteStudent(int id)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Students WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);

        int rowsChanged = command.ExecuteNonQuery();
        return rowsChanged > 0;
    }

    // IMPORT: save many students at once, inside one transaction.
    // Gives back how many students were actually added.
    // Students whose number is already saved are skipped (not added twice).
    public static int ImportStudents(List<Student> students)
    {
        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction();

        int addedCount = 0;

        foreach (Student student in students)
        {
            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT OR IGNORE INTO Students (StudentNumber, FullName, Course, YearLevel)
                VALUES ($studentNumber, $fullName, $course, $yearLevel);";
            command.Parameters.AddWithValue("$studentNumber", student.StudentNumber);
            command.Parameters.AddWithValue("$fullName", student.FullName);
            command.Parameters.AddWithValue("$course", student.Course);
            command.Parameters.AddWithValue("$yearLevel", student.YearLevel);

            int rowsChanged = command.ExecuteNonQuery();
            addedCount = addedCount + rowsChanged;
        }

        transaction.Commit();
        return addedCount;
    }

    // Helper: turns the rows returned by a SELECT into a List of Student objects
    private static List<Student> ReadStudents(SqliteCommand command)
    {
        List<Student> students = new List<Student>();

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Student student = new Student
            {
                Id = reader.GetInt32(0),
                StudentNumber = reader.GetString(1),
                FullName = reader.GetString(2),
                Course = reader.GetString(3),
                YearLevel = reader.GetInt32(4)
            };
            students.Add(student);
        }

        return students;
    }
}
