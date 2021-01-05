using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using coreapi.Comman;
using coreapi.Core;
using coreapi.LogApiHandler;
using coreapi.Models;
using coreapi.MongoModel;
using coreapi.MongoModel.QueryModel;
using coreapi.MongoModel.Settings;
using coreapi.MongoModel.TableName;
using coreapi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace coreapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LogController : CustomBaseController
    {
        
        private readonly LogRepository _logRepository;
        readonly IOptions<AppSettings> _appsettings;
        public LogController(IRepository<LogEventData, QueryLogModel> logRepository, IOptions<AppSettings> appsettings)
        {
            _logRepository = (LogRepository)logRepository;
            _appsettings = appsettings;
        }

        /// <summary>
        /// <![CDATA[trace]]>
        /// </summary>
        /// <param name="value"></param>
        [Route("trace")]
        [HttpPost]
        public void Trace([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("debug")]
        [HttpPost]
        public void Debug([FromBody] LogEventData value)
        {
            Add(value);

        }
        [Route("info")]
        [HttpPost]
        public void Info([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("warn")]
        [HttpPost]
        public void Warn([FromBody] LogEventData value)
        {
            Add(value);
        }
        /// <summary>
        /// <![CDATA[error]]>
        /// </summary>
        /// <param name="value"></param>
        [Route("error")]
        [HttpPost]
        public void Error([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("fatal")]
        [HttpPost]
        public void Fatal([FromBody] LogEventData value)
        {
            Add(value);
        }
        private async void Add(LogEventData data)
        {
            if (data != null)
            {
                await _logRepository.AddOne(data);
                if (!string.IsNullOrEmpty(data.Emails))
                {
                    new EmailHelpers(_appsettings).SendMailAsync(data.Emails, "监测邮件", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
        }
        [Route("delete")]
        [HttpPost]
        public async Task<bool> Delete([FromBody] LogEventData value)
        {
            return await _logRepository.RemoveOne(value);
        }

        [Route("get")]
        [HttpPost]
        public async Task<LogEventData> Get([FromBody] LogEventData value)
        {
            return await _logRepository.GetOne(value);
        }

        [Route("update")]
        [HttpPost]
        public async Task<bool> Update([FromBody] LogEventData value)
        {
            return await _logRepository.UpdateOne(value);
        }


        [Route("getpagelist")]
        [HttpPost]
        public async Task<ResponseModel<IEnumerable<LogEventData>>> GetPageList([FromQuery] QueryLogModel model)
        {
            ResponseModel<IEnumerable<LogEventData>> resp = new ResponseModel<IEnumerable<LogEventData>>
            {
                Data = await _logRepository.GetPageList(model)
            };
            return resp;
        }

        [Route("getalllist")]
        [HttpPost]
        public async Task<ResponseModel<IEnumerable<LogEventData>>> GetAllList()
        {
            ResponseModel<IEnumerable<LogEventData>> resp = new ResponseModel<IEnumerable<LogEventData>>
            {
                Data = await _logRepository.GetAllList()
            };
            return resp;
        }
    }
}