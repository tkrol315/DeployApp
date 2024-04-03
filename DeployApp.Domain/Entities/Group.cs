namespace DeployApp.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<InstanceGroup> InstanceGroups { get; set; }
    }
}
