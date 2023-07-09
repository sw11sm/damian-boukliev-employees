namespace Models;

public class EmployeeWorkData
{
    public int EmpID { get; set; }
    public int ProjectID { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
}