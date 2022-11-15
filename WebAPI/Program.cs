using WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("10.0.10.6"), 7163));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
