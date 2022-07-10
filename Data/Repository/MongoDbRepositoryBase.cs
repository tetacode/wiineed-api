using Core.Repository.Abstract;
using Data.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace Data.Repository;

public abstract class MongoDbRepositoryBase<T> : IRepository<T, string> where T : MongoDbEntity, new()
{
    protected readonly IMongoCollection<T> Collection;
    private readonly MongoDbSettings settings;
    private MongoClient _client;

    private IClientSessionHandle _session;
    private readonly ILogger<MongoDbRepositoryBase<T>> _logger;

    protected MongoDbRepositoryBase(IOptions<MongoDbSettings> options, ILogger<MongoDbRepositoryBase<T>> logger)
    {
        _logger = logger;
        settings = options.Value;

        var clientSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
        clientSettings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                _logger.LogWarning($"{e.CommandName} - {e.Command.ToJson()}");
            });
        };
        
        _client = new MongoClient(clientSettings);
        
        var db = _client.GetDatabase(settings.Database);
        Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
    
    public T Add(T data)
    {
        var options = new InsertOneOptions {BypassDocumentValidation = false};
        Collection.InsertOne(data, options);
        return data;
    }

    public void AddRange(IEnumerable<T> data)
    {
        var options = new InsertManyOptions() {IsOrdered = false, BypassDocumentValidation = false};
        Collection.InsertMany(data, options);
    }

    public T Update(T data)
    {
        return Collection.FindOneAndReplace(x => x.Id == data.Id, data);
    }

    public void UpdateRange(IEnumerable<T> data)
    {
        try
        {
            BeginTransaction();
            foreach (var entity in data)
            {
                Collection.FindOneAndReplace(x => x.Id == entity.Id, entity);
            }
            CommitTransaction();
        }
        catch (Exception e)
        {
            RollbackTransaction();
            throw;
        }
    }

    public void Delete(T data)
    {
        Collection.FindOneAndDelete(x => x.Id == data.Id);
    }

    public void DeleteRange(IEnumerable<T> data)
    {
        foreach (var entity in data)
        {
            Collection.FindOneAndDelete(x => x.Id == entity.Id);
        }
    }

    public IQueryable<T> Query()
    {
        return Collection.AsQueryable();
    }

    public void BeginTransaction()
    {
        _session = _client.StartSession();
        _session.StartTransaction();
    }

    public void CommitTransaction()
    {
        _session.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _session.AbortTransaction();
    }
}