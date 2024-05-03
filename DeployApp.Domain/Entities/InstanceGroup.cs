namespace DeployApp.Domain.Entities
{
    public class InstanceGroup
    {
        public Guid InstanceId { get; set; }
        public Instance Instance { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
