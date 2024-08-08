using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo
{
    public class MongoService : IMongoService
    {
        public ChallengeDatabaseSettings Settings { get; set; }
        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoService(IOptions<ChallengeDatabaseSettings> settings)
        {
            Settings = settings.Value;
            Client = new MongoClient(Settings.ConnectionString);
            Database = Client.GetDatabase(Settings.DatabaseName);
        }
    }
}
