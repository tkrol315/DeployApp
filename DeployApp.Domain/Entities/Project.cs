namespace DeployApp.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string YtCode { get; set; }
        public string RepositoryUrl { get; set; }
        public ICollection<Instance> Instances { get; set; }
        public ICollection<ProjectVersion> ProjectVersions { get; set; }
    }
}
