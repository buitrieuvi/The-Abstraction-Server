namespace TheAbstraction.Infrastructure.Data;

public class MongoDatabaseSettings
{ 
    public string PlayerCollectionName { get; set; } = null!;
    public string ItemCollectionName { get; set; } = null!;

    public string ConnectionString { get; set; } = null!;
 
    public string DatabaseName { get; set; } = null!;
}