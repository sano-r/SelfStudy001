using Microsoft.OpenApi.Models;
using PizzaStore.DB;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "PizzaStore API",
        Description = "Making the Pizza you love",
        Version = "v1"
    });
});

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}

//app.MapGet("/", () => "Hello World!");

app.MapGet("/pizzas/{id}", (int id) => PizzaDb.GetPizza(id)); // idに紐づくピザを取得
app.MapGet("/pizzas", () => PizzaDb.GetPizzas()); // すべてのピザを取得
app.MapPost("/pizzas", (Pizza pizza) => PizzaDb.CreatePizza(pizza)); // 新しいピザを追加
app.MapPut("/pizzas", (Pizza pizza) => PizzaDb.UpdatePizza(pizza)); // ピザを更新
app.MapDelete("/pizzas/{id}", (int id) => PizzaDb.RemovePizza(id)); // idに紐づくピザを削除

app.Run();
