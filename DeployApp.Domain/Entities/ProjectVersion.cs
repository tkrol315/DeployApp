namespace DeployApp.Domain.Entities
{
    public class ProjectVersion
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }  
        public int Patch { get; set; }
        public string Description { get; set; }
        public ICollection<Instance> Instances { get; set; }
        public ICollection<Deploy> Deploys { get; set; }
    }
}
