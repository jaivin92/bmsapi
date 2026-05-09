using bmsapi.Helpers;
using bmsmodel.FiltersAndAttributes;
using bmslib.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BMS API",
        Version = "v1"
    });

    options.SchemaFilter<SwaggerExcludePropertySchemaFilter>();
});

var appConfig = builder.Configuration.Get<AppConfig>();

builder.Services.AddSingleton(appConfig);

// case sensitive and no camel case
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

# region Model and Error Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<bmsapi.Helpers.Filters.ExceptionFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Error",
            Detail = "One or more validation errors occurred.",
            Instance = context.HttpContext.Request.Path
        };

        var _errorObj = new Error()
        {
            Status = false,
            ErrorType = bmslib.Enmus.ErrorType.Exception,
            Message = string.Join(", ", problemDetails.Errors.Select(t => string.Join(",", t.Value)))
        };

        return new BadRequestObjectResult(_errorObj);
    };
});

# endregion

bmsservice.Common.DependencyConfig.Configure(builder.Services, appConfig);

var app = builder.Build();

if (appConfig.CorsPolicy?.Count() > 0)
{
    app.UseCors((policy) =>
    {
        foreach (var item in appConfig.CorsPolicy)
        {
            policy.WithOrigins(item.Origins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()
            .WithExposedHeaders("Token");
        }
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
