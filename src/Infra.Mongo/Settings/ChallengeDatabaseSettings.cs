namespace Infra.Mongo.Settings
{
    public class ChallengeDatabaseSettings : IChallengeDatabaseSettings
    {
        public string KafkaMessagesCollectionName { get; set; }
        public string MotorcycleCollectionName { get; set; }
        public string RenterCollectionName { get; set; }
        public string RentalCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IChallengeDatabaseSettings
    {
        string MotorcycleCollectionName { get; set; }
        string RenterCollectionName { get; set; }
        string RentalCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}