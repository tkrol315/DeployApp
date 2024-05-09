using DeployApp.Domain.Enums;

namespace DeployApp.Domain.Entities
{
    public class DeployLog
    {
        public int Id { get; set; }
        public int DeployId { get; set; }
        public Guid InstanceId { get; set; }
        public DeployInstance DeployInstance { get; set; }
        public DateTime TimeStamp { get; set; }
        public Status Status { get; set; }
        public string Log { get; set; }
    }
}
