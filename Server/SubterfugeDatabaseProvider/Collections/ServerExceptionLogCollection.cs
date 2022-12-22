﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SubterfugeDatabaseProvider.Models;

namespace SubterfugeServerConsole.Connections.Collections;

public class ServerExceptionLogCollection: IDatabaseCollection<DbServerException>
{
    private IMongoCollection<DbServerException> _collection;

    public ServerExceptionLogCollection(IMongoCollection<DbServerException> collection)
    {
        this._collection = collection;
    }

    public async Task Upsert(DbServerException item)
    {
        await _collection.ReplaceOneAsync(
            it => it.Id == item.Id,
            item,
            new UpdateOptions { IsUpsert = true }
        );
    }

    public async Task Delete(DbServerException item)
    {
        await _collection.DeleteOneAsync(it => it.Id == item.Id);
    }

    public IMongoQueryable<DbServerException> Query()
    {
        return _collection.AsQueryable();
    }

    public async Task CreateIndexes()
    {
        await _collection.Indexes.CreateManyAsync( new List<CreateIndexModel<DbServerException>>()
        {
                new CreateIndexModel<DbServerException>(Builders<DbServerException>.IndexKeys.Ascending(action => action.Id)),
                new CreateIndexModel<DbServerException>(Builders<DbServerException>.IndexKeys.Ascending(action => action.Username)),
                new CreateIndexModel<DbServerException>(Builders<DbServerException>.IndexKeys.Ascending(action => action.UserId)),
                new CreateIndexModel<DbServerException>(Builders<DbServerException>.IndexKeys.Ascending(action => action.RequestUri)),
                new CreateIndexModel<DbServerException>(Builders<DbServerException>.IndexKeys.Ascending(action => action.RemoteIpAddress)),
                new CreateIndexModel<DbServerException>(Builders<DbServerException>.IndexKeys.Ascending(action => action.ExceptionSource)),
                new CreateIndexModel<DbServerException>(keys: Builders<DbServerException>.IndexKeys.Ascending(room => room.ExpireAt), options: new CreateIndexOptions()
                {
                    ExpireAfter = TimeSpan.FromSeconds(0),
                    Name = "ExpireAtIndex"
                }),
        });
    }

    public void Flush()
    {
        _collection.DeleteMany(FilterDefinition<DbServerException>.Empty);
    }
}