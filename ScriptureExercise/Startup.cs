using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using ScriptureExercise.Services;

namespace ScriptureExercise
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
			services.AddControllersWithViews();
			services.AddStackExchangeRedisCache(options =>
				options.Configuration = Configuration["RedisConfig:reid11"]
			);

			services.AddTransient<IMemoryCacheRepository, MemoryCacheRepository>();
			services.AddHttpContextAccessor();


			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				////設定登入Action的路徑： 
				//options.LoginPath = new PathString("/Account/Login");

				////設定 導回網址 的QueryString參數名稱：
				//options.ReturnUrlParameter = "ReturnUrl";

				////設定登出Action的路徑： 
				//options.LogoutPath = new PathString("/Account/Logout");

				////若權限不足，會導向的Action的路徑
				//options.AccessDeniedPath = new PathString("/Account/AccessDenied");
			})
			//加各家OAuth
			.AddGoogle(options => {
				var provider = "Google";
				options.ClientId = Configuration[$"Authentication:{provider}:ClientId"];
				options.ClientSecret = Configuration[$"Authentication:{provider}:ClientSecret"];

				//options.CallbackPath = "/signin-google";
			})
			.AddMicrosoftAccount(options => {
				var provider = "Microsoft";
				options.ClientId = Configuration[$"Authentication:{provider}:ClientId"];
				options.ClientSecret = Configuration[$"Authentication:{provider}:ClientSecret"];

				//options.CallbackPath = "/signin-microsoft";
			})
			.AddFacebook(options =>
			{
				var provider = "Facebook";
				options.AppId = Configuration[$"Authentication:{provider}:ClientId"];
				options.AppSecret = Configuration[$"Authentication:{provider}:ClientSecret"];

				//options.CallbackPath = "/signin-facebook";
			})
			.AddInstagram(options => {
				var provider = "Instagram";
				options.ClientId = Configuration[$"Authentication:{provider}:ClientId"];
				options.ClientSecret = Configuration[$"Authentication:{provider}:ClientSecret"];

				//options.CallbackPath = "/signin-github";
			})
			.AddLine(options =>
			{
				var provider = "Line";
				options.ClientId = Configuration[$"Authentication:{provider}:ClientId"];
				options.ClientSecret = Configuration[$"Authentication:{provider}:ClientSecret"];

				//options.CallbackPath = "/signin-line";
			})
			.AddGitHub(options => {
				var provider = "GitHub";
				options.ClientId = Configuration[$"Authentication:{provider}:ClientId"];
				options.ClientSecret = Configuration[$"Authentication:{provider}:ClientSecret"];

				//options.CallbackPath = "/signin-github";
				//OAuth請求的【redirect_uri參數】，套件預設值
				//使用者授權給個資之後，要回呼的網址。官方要註冊APP，允許這個回呼網址


				//預設：
				//  會請求的scope：
				//  回傳的claim： 

				//額外：(從官方文件看，請求更多的可用scope )
				//options.Scope.Add("");
				//  回傳的claim：
			});


			//swagger 自己好像不需要...
			//vue bsVue





			services.AddTransient<IAccountService, AccountService>();
			services.AddTransient<IMemberService, MemberService>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
