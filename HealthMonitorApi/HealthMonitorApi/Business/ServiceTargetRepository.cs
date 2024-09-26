using HealthMonitorApi.Models;
using MongoDB.Driver;
using System.Text.Json;

namespace HealthMonitorApi.Business
{
    public class ServiceTargetRepository : IServiceTargetRepository
    {
        private readonly ILogger<ServiceTargetRepository> _logger;
        private readonly IMongoCollection<ServiceTargetGroup> _data;

        public ServiceTargetRepository(ILogger<ServiceTargetRepository> logger,
            MongoClient mongoDb,
            IConfiguration configuration)
        {
            _logger = logger;

            string connStr = configuration["MongoDB:ConnectionString"]!;
            string databaseName = configuration["MongoDB:DatabaseName"]!;
            string collectionName = configuration["MongoDB:ServiceTargetsCollectionName"]!;
            _data = mongoDb.GetDatabase(databaseName).GetCollection<ServiceTargetGroup>(collectionName);
        }

        public async Task<List<ServiceTargetGroup>> FetchAllGroups(Guid transaction)
        {
            _logger.LogTrace($"{transaction} - in {nameof(FetchAllGroups)}");

            try
            {
                var documentsResult = await _data.FindAsync(_ => true);
                List<ServiceTargetGroup> documents = await documentsResult.ToListAsync();
                return documents;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving documents");
            }
            return null;
        }

        public async Task AddGroup(Guid transaction, ServiceTargetGroup group)
        {
            _logger.LogTrace($"{transaction} - in {nameof(AddGroup)}");
            
            try
            {
                await _data.InsertOneAsync(group);
                _logger.LogInformation($"{transaction} - added group");
                _logger.LogDebug(JsonSerializer.Serialize(group));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding document");
            }
        }
    }
}