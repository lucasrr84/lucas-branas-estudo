using invoice.src.application.usecase;
using invoice.src.domain.service;
using invoice.src.infra.queue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o serviço
builder.Services.AddSingleton<IQueue, RabbitMQAdapter>();
builder.Services.AddSingleton<GenerateInvoice>();
builder.Services.AddHostedService<MessageConsumer>();
builder.Services.AddSingleton<MessageConsumer>();


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
app.Run("http://localhost:3002");