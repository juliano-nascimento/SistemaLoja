using System;
using AutoMapper;
using Loja.Domain.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Loja.Business.Interfaces;
using Loja.Repository.Interfaces;
using Loja.Repository.Implementations;
using Loja.Business.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Loja.Portal
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/Login/Index");

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(5);
            });
            services.AddControllersWithViews();
            services.AddDbContext<dblojaContext>(options =>
                                                    options.UseMySql(Configuration.GetConnectionString("Connection")));
            //Interfaces Business
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
            services.AddScoped<IFornecedorBusiness, FornecedorBusiness>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            services.AddScoped<IPedidoBusiness, PedidoBusiness>();
            services.AddScoped<ILancamentoBusiness, LancamentoBusiness>();
            //Interfaces Repository
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
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
                app.UseExceptionHandler("/Dashboard/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
