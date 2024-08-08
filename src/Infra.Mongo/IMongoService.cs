using Infra.Mongo.Settings;
using MongoDB.Driver;

namespace Infra.Mongo
{
    public interface IMongoService
    {
        ChallengeDatabaseSettings Settings { get; set; }
        IMongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
    }
}
