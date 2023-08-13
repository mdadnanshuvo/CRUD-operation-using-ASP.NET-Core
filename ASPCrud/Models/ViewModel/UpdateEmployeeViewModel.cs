namespace ASPCrud.Models.ViewModel
{
    public class UpdateEmployeeViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Department { get; set; }
    }
}
