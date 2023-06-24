namespace Pizza.Repository
{
    public class MongoDBSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? MenuCollectionName { get; set; }
        public string? CustomerCollectionName { get; set; }
    }
}
