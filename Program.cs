using WebApi.Authorization;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

//if (!app.Environment.IsDevelopment()) {
//app.UseExceptionHandler("/Error");
//// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//app.UseHsts();
//}


// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom basic auth middleware
    app.UseMiddleware<BasicAuthMiddleware>();

    app.MapControllers();

    // ADDED BY SAYED FOR DEBUGGING ONLY
    app.UseExceptionHandler("/Error");
}

app.Run();