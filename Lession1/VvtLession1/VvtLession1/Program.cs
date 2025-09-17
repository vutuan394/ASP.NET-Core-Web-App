var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World! - Vũ Văn Tuấn - 2210900132");

app.Run();
