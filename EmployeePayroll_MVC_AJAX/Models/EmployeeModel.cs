namespace EmployeePayroll_MVC_AJAX.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeModel
    {

        [Key]
        public int Emp_Id { get; set; }

        [Required]
        [RegularExpression("[A-Z]{1}[a-z]{3,}", ErrorMessage = "Please Enter for Employee Name Atleast 3 character with first letter capital")]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Department { get; set; }

        public string Notes { get; set; }
    }
}
