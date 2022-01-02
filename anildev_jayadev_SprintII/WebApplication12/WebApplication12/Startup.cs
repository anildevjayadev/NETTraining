using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Repository.Models;
using Business;
using Microsoft.EntityFrameworkCore;

namespace WebApplication12
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

            services.AddDbContext<AppDbContext>(context => { context.UseInMemoryDatabase("WebApplication12"); });
            services.AddScoped<IUser, UserImplementation>();
            services.AddScoped<IProjects, ProjectImplementation>();
            services.AddScoped<ITask,TasksImplementation>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication12", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication12 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            TestData(context);
        }
        public static void TestData(AppDbContext context)
        {
           //Test Project Data
            context.Projects.Add(new Project { ProjectId = 1, ProjectName = "Student Project", ProjectDetail = "Project for Student management", ProjectCreatedOn = DateTime.Now });
            context.Projects.Add(new Project { ProjectId = 2, ProjectName = "Employee Project", ProjectDetail = "Project for Employee Management", ProjectCreatedOn = DateTime.Now });
            context.Projects.Add(new Project { ProjectId = 3, ProjectName = "Employer Project", ProjectDetail = "Project for Employer Management", ProjectCreatedOn = DateTime.Now });

            context.Tasks.Add(new TasksModel { ID = 1, ProjectId = 1, Status = 2, TaskassignedtoUserId = 1, Details = "This is a test task", CreatedOn = DateTime.Now });
            context.Tasks.Add(new TasksModel { ID = 2, ProjectId = 1, Status = 3, TaskassignedtoUserId = 2, Details = "This is a test task", CreatedOn = DateTime.Now });
            context.Tasks.Add(new TasksModel { ID = 3, ProjectId = 2, Status = 4, TaskassignedtoUserId = 2, Details = "this is a test task", CreatedOn = DateTime.Now });

            context.Users.Add(new User { UserID = 1, UserEmail = "mark.tan@gmail.com", Password = "mark@123", FirstName = "Mark", LastName = "Tan" });
            context.Users.Add(new User{ UserID = 2, UserEmail = "scott.pan@gmail.com", Password = "scott@123", FirstName = "Scott", LastName = "Pan" });
            context.Users.Add(new User{ UserID = 3, UserEmail = "tom.schmidt@gmail.com", Password = "tom@123", FirstName = "Tom", LastName = "Schmidt" });
            
            context.SaveChanges();
        }
    }
}
