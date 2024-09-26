using HealthMonitorApi.Models;

namespace HealthMonitorApi.Business
{
    public interface IServiceTargetRepository
    {
        public Task<List<ServiceTargetGroup>> FetchAllGroups(Guid transaction);
        public Task AddGroup(Guid transaction, ServiceTargetGroup group);
    }
}
