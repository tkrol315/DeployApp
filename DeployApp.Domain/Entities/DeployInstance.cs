using DeployApp.Domain.Enums;

namespace DeployApp.Domain.Entities
{
    public class DeployInstance
    {
        public int DeployId { get; set; }
        public Deploy Deploy { get; set; }
        public int InstanceId { get; set; }
        public Instance Instance { get; set; }
        public Status Status { get; set; }
    }
}
