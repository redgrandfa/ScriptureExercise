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
				////�]�w�n�JAction�����|�G 
				//options.LoginPath = new PathString("/Account/Login");

				////�]�w �ɦ^���} ��QueryString�ѼƦW�١G
				//options.ReturnUrlParameter = "ReturnUrl";

				////�]�w�n�XAction�����|�G 
				//options.LogoutPath = new PathString("/Account/Logout");

				////�Y�v�������A�|�ɦV��Action�����|
				//options.AccessDeniedPath = new PathString("/Account/AccessDenied");
			})
			//�[�U�aOAuth
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
				//OAuth�ШD���iredirect_uri�Ѽơj�A�M��w�]��
				//�ϥΪ̱��v���Ӹꤧ��A�n�^�I�����}�C�x��n���UAPP�A���\�o�Ӧ^�I���}


				//�w�]�G
				//  �|�ШD��scope�G
				//  �^�Ǫ�claim�G 

				//�B�~�G(�q�x����ݡA�ШD��h���i��scope )
				//options.Scope.Add("");
				//  �^�Ǫ�claim�G
			});


			//swagger �ۤv�n�����ݭn...
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
