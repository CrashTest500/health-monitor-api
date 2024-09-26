namespace HealthMonitorApi.Models
{
    public class ServiceTargetGroup
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public List<ServiceTarget> Targets { get; set; } = new List<ServiceTarget>();
    }

    public class ServiceTarget
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
    }
}
