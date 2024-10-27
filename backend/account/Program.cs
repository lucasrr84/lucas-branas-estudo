using account.src.application.usecase;
using account.src.infra.gateway;
using account.src.infra.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o serviço
builder.Services.AddScoped<IMailerGateway, MailerGatewayMemory>();
builder.Services.AddSingleton<IAccountRepository, AccountRepositoryMemory>();
builder.Services.AddScoped<Signup>();
builder.Services.AddScoped<GetAccount>();

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
app.Run("http://localhost:3001");