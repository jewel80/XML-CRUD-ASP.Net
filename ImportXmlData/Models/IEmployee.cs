namespace ImportXmlData.Models
{
    public interface IEmployee
    {
        IEnumerable<EmployeeModel> All();
        EmployeeModel Create(EmployeeModel emp);
        EmployeeModel Update(EmployeeModel updateEmp);
        EmployeeModel Delete(int? id);
        EmployeeModel GetById(int? id);

    }
}
