using ApiBotDiscord.Infraestrutura;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Carregar variaveis do arquivo .env
DotEnv.Load(); // Adicionando esta linha para carregar as variaveis de ambiente

// Add services to the container.
// JWT
string? secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); // Obt�m a chave do .env
var AllowSpecificOrigin = "_MyAllowSpecificOrigins"; // Nome da pol�tica de CORS
var AllowGetOnly = "_MyAllowGetOnly"; // Nome da pol�tica de CORS
var allowList = Environment.GetEnvironmentVariable("CORS_ORIGIN_WHITELIST");
System.Console.WriteLine("a lista é " + allowList);

System.Console.WriteLine("o segredo é " + secretKey);
if (string.IsNullOrEmpty(secretKey))
{
    secretKey = "your-default-secret-key";  // Chave padrão
}
System.Console.WriteLine("o segredo agora é " + secretKey);
var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Personagens API", Version = "v1" });

    // Adicionando o esquema de seguran�a JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autoriza��o JWT usando o esquema Bearer. \n\n" +
                      "Insira 'Bearer' [espa�o] e depois o token JWT.\n\n" +
                      "Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FranquiaContext>();
builder.Services.AddDbContext<PersonagemContext>();
builder.Services.AddDbContext<ContaContext>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigin,
        policy =>
        {
            policy.WithOrigins(allowList) // Substitua com o IP ou dom�nio espec�fico
                   .AllowAnyHeader()
                   .AllowAnyMethod(); // Permite qualquer m�todo para esse site espec�fico
        });

    options.AddPolicy(name: AllowGetOnly,
        policy =>
        {
            policy.AllowAnyOrigin() // Permite qualquer dom�nio
                   .AllowAnyHeader()
                   .WithMethods("GET"); // Permite apenas requisi��es GET
        });
});

builder.Services.AddControllers();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();
app.UseRouting();

var sitePermitido = Environment.GetEnvironmentVariable("Site_permitido");
System.Console.WriteLine("o site permitido é " + sitePermitido);
app.UseCors(policy =>
{
    policy.WithOrigins(sitePermitido) // Para este site espec�fico
          .AllowAnyHeader()
          .AllowAnyMethod() // Permitir qualquer m�todo
          .SetIsOriginAllowedToAllowWildcardSubdomains()
          .AllowCredentials(); // Permitir envio de cookies (se necess�rio)
});
app.UseCors(AllowSpecificOrigin);

app.UseCors(AllowGetOnly);

app.UseAuthorization();

app.MapControllers();

app.Urls.Add("http://localhost:80");

app.Run();
