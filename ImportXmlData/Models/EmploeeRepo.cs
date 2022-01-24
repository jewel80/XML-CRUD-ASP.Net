namespace ImportXmlData.Models
{
    public class EmploeeRepo : IEmployee
    {
        private readonly AppDataContext context;
        public EmploeeRepo(AppDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<EmployeeModel> All()
        {
            return context.employeeModels.ToList();
        }

        public EmployeeModel Create(EmployeeModel emp)
        {
            context.employeeModels.Add(emp);
            context.SaveChanges();
            return emp;
        }

        public EmployeeModel Delete(int? id)
        {
            var model = GetById(id);
            context.employeeModels.Remove(model);
            context.SaveChanges();
            return model;
        }

        public EmployeeModel GetById(int? id)
        {
            return context.employeeModels.FirstOrDefault(e=> e.Id==id);
        }

        public EmployeeModel Update(EmployeeModel updateEmp)
        {
            var model = context.employeeModels.Attach(updateEmp);
            model.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updateEmp;
        }
    }
}
