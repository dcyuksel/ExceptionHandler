using ExceptionHandler.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<NotImplementedExceptionHandler>();
builder.Services.AddExceptionHandler<NotSupportedExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseExceptionHandler();

app.MapGet("/exception",
    () =>
    {
        throw new Exception("This is an exception!");
    })
.WithName("Exception")
.WithOpenApi();

app.MapGet("/notImplementedException",
    () =>
    {
        throw new NotImplementedException("This feature has not been implemented yet!");
    })
.WithName("NotImplementedException")
.WithOpenApi();

app.MapGet("/notSupportedException",
    () =>
    {
        throw new NotSupportedException("This feature is not supported!");
    })
.WithName("NotSupportedException")
.WithOpenApi();

app.Run();