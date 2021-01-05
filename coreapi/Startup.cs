using System;
using System.IO;
using System.Reflection;
using System.Text;
using Core.Common.Logger;
using coreapi.Exceptions;
using coreapi.LogApiHandler;
using coreapi.MongoModel.QueryModel;
using coreapi.MongoModel.Settings;
using coreapi.MongoModel.TableName;
using coreapi.Repository;
using coreapi.SQLContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApiContrib.Core.Formatter.MessagePack;

namespace coreapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            new LoggerFactory().AddFile(Configuration.GetSection("FileLogging"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //web api 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "接口文档",
                    Description = "RESTful API",
                    Contact = new OpenApiContact
                    {
                        Name = "谢昆明",
                        Email = "1477165313@qq.com",
                        Url = new Uri("https://mp.weixin.qq.com/s/t-ayt-f9iZodwZP0TeO4zw")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // 为 Swagger 设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            //jwt验证：
            var JwtSettings = Configuration.GetSection("JwtSettings");
            var Audience = JwtSettings["Audience"];
            var Issuer = JwtSettings["Issuer"];
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecurityKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = Audience,//Audience
                    ValidIssuer = Issuer,//Issuer，这两项和签发jwt的设置一致
                    IssuerSigningKey = SecurityKey, //拿到SecurityKey
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });



            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod();
                });
            });

            ////设置数据库
            var MsSqlConn = Configuration.GetConnectionString("MsSqlConn");
            var MySqlConn = Configuration.GetConnectionString("MySqlConn");
            var MongoDBConn = Configuration.GetSection("MongoConnectionStrings");
            var AppSettings = Configuration.GetSection("AppSettings");
            services.AddDbContext<MSSQLContext>(option => option.UseSqlServer(MsSqlConn));
            services.AddDbContext<MYSQLContext>(option => option.UseMySql(MySqlConn));
            services.Configure<DBSettings>(MongoDBConn);//数据库连接信息
            services.Configure<AppSettings>(AppSettings);//其他配置信息


            //仓库注册
            //为了通过DI model来访问LogRepository，修改Startup.cs ，ConfigureServices添加如下代码
            services.AddTransient<IRepository<LogEventData, QueryLogModel>, LogRepository>();//数据访问

            //MongoDB Logger  日志
            ////messagepack可以让我们在post数据的时候序列化数据，“压缩”数据传输大小,这个会结合针对接口封装的类库配合使用。
            services.AddMvcCore().AddMessagePackFormatters();
            services.AddMvc().AddMessagePackFormatters();
            //logger
            services.AddLoggerService("TestApi");


            services.AddControllers();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<DBSettings> settings)
        {
            //MongoDB Logger  日志
            app.ConfigureExceptionHandler(settings);
            //https://www.cnblogs.com/betterlife/p/9676033.html  logger mongodb

            //授权处理
            //app.UseMiddleware(typeof(RequestAuthorizeMiddleware));



            //跨域
            app.UseCors("AnyOrigin");

            ////https://www.zhangshengrong.com/p/QrXejlz41d/
            //启用中间件服务生成Swagger
            app.UseSwagger();
            //启用中间件服务生成SwaggerUI，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Api V1");
                c.RoutePrefix = string.Empty;//设置根节点访问
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            //UseAuthentication 必须在 UseAuthorization 前面 
            //配置授权
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
