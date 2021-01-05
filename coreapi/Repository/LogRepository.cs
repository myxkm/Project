using coreapi.LogApiHandler;
using coreapi.MongoModel.QueryModel;
using coreapi.MongoModel.Settings;
using coreapi.MongoModel.TableName;
using coreapi.SQLContext;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace coreapi.Repository
{
    public class LogRepository : IRepository<LogEventData,QueryLogModel>
    {
        private readonly MongoContext _context = null;
        public LogRepository(IOptions<DBSettings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task AddOne(LogEventData item)
        {
            if (string.IsNullOrWhiteSpace(item.CurrentUniqueId))
            {
                item.CurrentUniqueId = ObjectId.GenerateNewId().ToString();

            }
            await _context.LogEventDatas.InsertOneAsync(item);
        }
        public async Task<IEnumerable<LogEventData>> GetPageList(QueryLogModel model)
        {
            var builder = Builders<LogEventData>.Filter;
            FilterDefinition<LogEventData> filter = builder.Empty;
            if (!string.IsNullOrEmpty(model.Level))
            {
                filter = builder.Eq("Level", model.Level);
            }
            if (!string.IsNullOrEmpty(model.LogSource))
            {
                filter &= builder.Eq("LogSource", model.LogSource);
            }
            if (!string.IsNullOrEmpty(model.Message))
            {
                filter &= builder.Regex("Message", new BsonRegularExpression(new Regex(model.Message)));
            }
            if (DateTime.MinValue != model.StartTime)
            {
                filter &= builder.Gte("Date", model.StartTime);
            }
            if (DateTime.MinValue != model.EndTime)
            {
                filter &= builder.Lte("Date", model.EndTime);
            }
            return await _context.LogEventDatas.Find(filter)
                 .SortByDescending(log => log.Date)
                 .Skip((model.PageIndex - 1) * model.PageSize)
                 .Limit(model.PageSize).ToListAsync();
        }
      
        public async Task<LogEventData> GetOne(LogEventData item)
        {
            return await _context.LogEventDatas.Find(t => t.CurrentUniqueId == item.CurrentUniqueId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LogEventData>> GetAllList()
        {
            var builder = Builders<LogEventData>.Filter;
            FilterDefinition<LogEventData> filter = builder.Empty;
            filter &= builder.Ne("CurrentUniqueId", "");
            return await _context.LogEventDatas.Find(filter)
               .SortByDescending(log => log.Date)
               .ToListAsync();
        }

        public async Task<bool> RemoveOne(LogEventData item)
        {
            var result = await _context.LogEventDatas.DeleteOneAsync(t => t.CurrentUniqueId == item.CurrentUniqueId);
            return result.DeletedCount > 0;
        }
        public async Task<bool> UpdateOne(LogEventData item)
        {
            var filter = Builders<LogEventData>.Filter.Eq(s => s.CurrentUniqueId, item.CurrentUniqueId);
            var update = Builders<LogEventData>.Update.Set(s => s.Date, item.Date);
            var result = await _context.LogEventDatas.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
        #region 未实现方法
        #endregion
    }
}
