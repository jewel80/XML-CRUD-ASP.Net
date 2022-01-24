using System.ComponentModel.DataAnnotations.Schema;

namespace ImportXmlData.Models
{
    [Table("Employees")]
    public class EmployeeModel
    {
       
        public int Id { get; set; }
        public string? EmployeeId { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Title { get; set; }

        public string? Division { get; set; }

        public string? Building { get; set; }

        public string? Room { get; set; }
    }
}
