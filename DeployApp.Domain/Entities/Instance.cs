namespace DeployApp.Domain.Entities
{
    public class Instance
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public string  Name { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public int? ProjectVersionId { get; set; }
        public ProjectVersion ProjectVersion { get; set; }
        public DeployInstance DeployInstance { get; set; }
        public ICollection<InstanceTag> InstanceTags { get; set; } 
        public ICollection<InstanceGroup> InstanceGroups { get; set; }

    }
}
