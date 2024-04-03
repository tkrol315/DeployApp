namespace DeployApp.Domain.Entities
{
    public class Type
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int InstanceId { get; set; }
        public Instance Instance { get; set; }
    }
}
