using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Configura YARP com leitura do appsettings + interceptação para debug
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(builderContext =>
    {
        builderContext.AddRequestTransform(transformContext =>
        {
            var path = transformContext.HttpContext.Request.Path;
            Console.WriteLine($"[Gateway] Interceptando requisição: {path}");
            return ValueTask.CompletedTask;
        });
    });

var app = builder.Build();


// Redirecionamento HTTPS (mantido como padrão)
//app.UseHttpsRedirection();

// Roteamento reverso via YARP (essencial)
app.MapReverseProxy();

app.Run();
