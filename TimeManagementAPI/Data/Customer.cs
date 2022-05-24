namespace TimeManagementAPI.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        
        public List<Project> Projects { get; set; }
    }
}
