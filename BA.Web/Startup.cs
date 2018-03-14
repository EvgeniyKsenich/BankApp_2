using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BA.Database.DataContext;
using AutoMapper;
using BA.Database.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BA.Web.Auth;
using Microsoft.IdentityModel.Tokens;
using DA.Business.Repositories;
using DA.Business.Servises;
using DA.Business.Utiles;
using BA.Business.Repositories;
using BA.Business.Utiles;
using BA.Business.Modeles;

namespace BA.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connection));

            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Modeles.Mapper());
            });
            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITransactionServisesRepositoryes, TransactionServises>();
            services.AddTransient<IUserServises, UserServises>();
            services.AddTransient<IViewModelEngine, ViewModelEngine>();
            services.AddTransient<IPasswordEngine, PasswordEngine>();

            services.Configure<Identity>(Configuration.GetSection("Identity"));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            var Identity = new Identity()
            {
                Audience = Configuration.GetValue<string>("Identity:Audience"),
                Key = Configuration.GetValue<string>("Identity:KEY"),
                LifeTime = Configuration.GetValue<int>("Identity:LifeTime"),
                Publisher = Configuration.GetValue<string>("Identity:Publisher"),
                Salt = Configuration.GetValue<string>("Identity:Salt")
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        { 
                            ValidateIssuer = true,
                            ValidIssuer = Identity.Publisher,

                            ValidateAudience = true,
                            ValidAudience = Identity.Audience,

                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(Identity.Key),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
