// Student.cs - the blueprint for a student

public class Student
{
    public string StudentNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Course { get; set; } = "";
    public int YearLevel { get; set; }

    public void Display()
    {
        Console.WriteLine($"[{StudentNumber}] {FullName} - {Course} {YearLevel}");
    }
}
