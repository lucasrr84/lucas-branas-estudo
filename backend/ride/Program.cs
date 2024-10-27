using ride.src.infra.queue;
using ride.src.infra.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o serviço
builder.Services.AddSingleton<IRideRepository, RideRepositoryMemory>();
builder.Services.AddSingleton<IPositionRepository, PositionRepositoryMemory>();
builder.Services.AddSingleton<IQueue, RabbitMQAdapter>();

// Adicionar serviços ao contêiner
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();
app.Run("http://localhost:3000");