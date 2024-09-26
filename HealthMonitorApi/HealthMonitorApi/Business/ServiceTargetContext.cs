using HealthMonitorApi.Models;

namespace HealthMonitorApi.Business
{
    public class ServiceTargetContext : IServiceTargetContext
    {
        private readonly IServiceTargetRepository _repo;

        public ServiceTargetContext(IServiceTargetRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ServiceTargetGroup>> GetGroups(Guid transaction)
        {
            return await _repo.FetchAllGroups(transaction);
        }

        public async Task AddGroup(Guid transaction, ServiceTargetGroup group)
        {
            await _repo.AddGroup(transaction, group);
        }
    }
}
