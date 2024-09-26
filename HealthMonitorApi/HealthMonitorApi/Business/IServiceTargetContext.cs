using HealthMonitorApi.Models;

namespace HealthMonitorApi.Business
{
    public interface IServiceTargetContext
    {
        public Task<List<ServiceTargetGroup>> GetGroups(Guid transaction);
        public Task AddGroup(Guid transaction, ServiceTargetGroup group);
    }
}
