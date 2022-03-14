using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using OrderIn.Middlewares;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OrderInBackend.Helpers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System.Configuration;
using OrderIn.Services.Twilio;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace OrderIn
{
    public class Startup
    {
        private ClassHelper _helper;
        public static readonly string cors = "all";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this._helper = new ClassHelper();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {

                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "ms"; //my session
            });


            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderIn", Version = "v1" });
            });

            services.AddCors(c => c.AddPolicy(

                name: cors,
                builder =>
                {
                    builder
                    .WithOrigins(
                        "https://localhost:4200",
                        "http://localhost:4200",
                        "https://orderin-chart.web.app"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                }
                ));

            services.AddMvc(o =>
            {

            }).AddFluentValidation(o =>
            {
                o.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                o.RegisterValidatorsFromAssemblyContaining<Startup>();
                o.ImplicitlyValidateChildProperties = true;
            }).AddControllersAsServices();

            //.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


            services.AddSingleton<IVerification>(new Verification(
                Configuration.GetSection("Twilio").Get<ConfigurationTwilio>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocExpansion(DocExpansion.None);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderIn v1");
                });

            }

            if (Convert.ToBoolean(Configuration["IsEncription"].ToString()))
            {
                //decrypt request body
                app.UseHttpRequestDecriptor();

                //encrypt response body
                app.UseHttpResponseEncryptor();
            }
            else
            {
                ////response tanpa enkripsi
                //app.Use(async (context, next) =>
                //{
                //    var response = context.Response;
                //    var originalBodyStream = response.Body;
                //    var responseBody = string.Empty;

                //    try
                //    {

                //        var cookieOptions = new CookieOptions
                //        {
                //            // Set the secure flag, which Chrome's changes will require for SameSite none.
                //            // Note this will also require you to be running on HTTPS.
                //            //Secure = true,

                //            // Set the cookie to HTTP only which is good practice unless you really do need
                //            // to access it client side in scripts.
                //            HttpOnly = false,
                //            //Domain = "https://localhost:4200",

                //            // Add the SameSite attribute, this will emit the attribute with a value of none.
                //            // To not emit the attribute at all set
                //            // SameSite = (SameSiteMode)(-1)
                //            SameSite = SameSiteMode.Lax,
                //            Path = "/"
                //        };
                //        //context.Response.Cookies.Append("tes", "value", cookieOptions);

                //        using (var newResponse = new MemoryStream())
                //        {
                //            context.Response.Body = newResponse;
                //            await next();

                //            response.Body.Seek(0, SeekOrigin.Begin);

                //            responseBody = await new StreamReader(response.Body).ReadToEndAsync();

                //            //string token = context.Request?.Headers["auth"];

                //            object result = new
                //            {
                //                result = JsonConvert.DeserializeObject(responseBody),
                //                //timeLimit = this._helper.TokenExpiration(token)
                //            };

                //            var responseData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                //            var stream = new MemoryStream(responseData);
                //            await stream.CopyToAsync(originalBodyStream);
                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //        //log error
                //    }
                //});
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseCors(cors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
