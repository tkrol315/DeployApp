namespace DeployApp.Domain.Entities
{
    public class Deploy
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ProjectVersionId { get; set; }
        public ProjectVersion ProjectVersion { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DeployInstance> DeployInstances { get; set; }
    }
}
