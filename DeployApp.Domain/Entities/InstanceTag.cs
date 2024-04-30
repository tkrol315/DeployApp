using System.Reflection.Metadata.Ecma335;

namespace DeployApp.Domain.Entities
{
    public class InstanceTag
    {
        public int InstanceId { get; set; }
        public Instance Instance { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
