using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Configura YARP com leitura do appsettings + intercepta��o para debug
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(builderContext =>
    {
        builderContext.AddRequestTransform(transformContext =>
        {
            var path = transformContext.HttpContext.Request.Path;
            Console.WriteLine($"[Gateway] Interceptando requisi��o: {path}");
            return ValueTask.CompletedTask;
        });
    });

var app = builder.Build();


// Redirecionamento HTTPS (mantido como padr�o)
//app.UseHttpsRedirection();

// Roteamento reverso via YARP (essencial)
app.MapReverseProxy();

app.Run();
