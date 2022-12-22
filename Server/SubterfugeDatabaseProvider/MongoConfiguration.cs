﻿namespace SubterfugeServerConsole.Connections;

public class MongoConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public Boolean CreateSuperUser { get; set; }
    public string SuperUserUsername { get; set; }
    public string SuperUserPassword { get; set; }
    public Boolean FlushDatabase { get; set; }
}

public interface IMongoConfigurationProvider
{
    public MongoConfiguration GetConfiguration();
}

public class DefaultMongoConfigurationProviderImpl : IMongoConfigurationProvider
{
    
    public MongoConfiguration Config { get; set; }
    
    public MongoConfiguration GetConfiguration()
    {
        return Config;
    }
}