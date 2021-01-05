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
            //web api �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "�ӿ��ĵ�",
                    Description = "RESTful API",
                    Contact = new OpenApiContact
                    {
                        Name = "л����",
                        Email = "1477165313@qq.com",
                        Url = new Uri("https://mp.weixin.qq.com/s/t-ayt-f9iZodwZP0TeO4zw")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
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

                // Ϊ Swagger ����xml�ĵ�ע��·��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            //jwt��֤��
            var JwtSettings = Configuration.GetSection("JwtSettings");
            var Audience = JwtSettings["Audience"];
            var Issuer = JwtSettings["Issuer"];
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecurityKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = Audience,//Audience
                    ValidIssuer = Issuer,//Issuer���������ǩ��jwt������һ��
                    IssuerSigningKey = SecurityKey, //�õ�SecurityKey
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });



            //���ÿ�����
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod();
                });
            });

            ////�������ݿ�
            var MsSqlConn = Configuration.GetConnectionString("MsSqlConn");
            var MySqlConn = Configuration.GetConnectionString("MySqlConn");
            var MongoDBConn = Configuration.GetSection("MongoConnectionStrings");
            var AppSettings = Configuration.GetSection("AppSettings");
            services.AddDbContext<MSSQLContext>(option => option.UseSqlServer(MsSqlConn));
            services.AddDbContext<MYSQLContext>(option => option.UseMySql(MySqlConn));
            services.Configure<DBSettings>(MongoDBConn);//���ݿ�������Ϣ
            services.Configure<AppSettings>(AppSettings);//����������Ϣ


            //�ֿ�ע��
            //Ϊ��ͨ��DI model������LogRepository���޸�Startup.cs ��ConfigureServices������´���
            services.AddTransient<IRepository<LogEventData, QueryLogModel>, LogRepository>();//���ݷ���

            //MongoDB Logger  ��־
            ////messagepack������������post���ݵ�ʱ�����л����ݣ���ѹ�������ݴ����С,���������Խӿڷ�װ��������ʹ�á�
            services.AddMvcCore().AddMessagePackFormatters();
            services.AddMvc().AddMessagePackFormatters();
            //logger
            services.AddLoggerService("TestApi");


            services.AddControllers();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<DBSettings> settings)
        {
            //MongoDB Logger  ��־
            app.ConfigureExceptionHandler(settings);
            //https://www.cnblogs.com/betterlife/p/9676033.html  logger mongodb

            //��Ȩ����
            //app.UseMiddleware(typeof(RequestAuthorizeMiddleware));



            //����
            app.UseCors("AnyOrigin");

            ////https://www.zhangshengrong.com/p/QrXejlz41d/
            //�����м����������Swagger
            app.UseSwagger();
            //�����м����������SwaggerUI��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Api V1");
                c.RoutePrefix = string.Empty;//���ø��ڵ����
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            //UseAuthentication ������ UseAuthorization ǰ�� 
            //������Ȩ
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
