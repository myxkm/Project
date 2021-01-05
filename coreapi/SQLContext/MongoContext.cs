using MongoDB.Driver;
using Microsoft.Extensions.Options;
using coreapi.MongoModel.Settings;
using coreapi.LogApiHandler;
using coreapi.MongoModel.TableName;

namespace coreapi.SQLContext
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly string _logCollection;
        public MongoContext(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoConn);
            if (client != null)
                _database = client.GetDatabase(settings.Value.MongoDBName);
            _logCollection = settings.Value.TableName_Logs;
        }

        public IMongoCollection<LogEventData> LogEventDatas
        {
            get
            {
                return _database.GetCollection<LogEventData>(_logCollection);
            }
        }
    }

}
