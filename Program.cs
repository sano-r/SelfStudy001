using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore.DB;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();

//メモリ内DBをSqlServerに置き換える
//builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSqlServer<PizzaDb>(connectionString);

builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "PizzaStore API",
        Description = "Making the Pizza you love",
        Version = "v1"
    });
});

// 1) 一意の文字列を定義
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// 2) 許可されたドメインを定義。 今回は"http://example.com" と "*" = all
// ドメインはテスト目的のみに使用。
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder => {
        builder.WithOrigins("http://example.com", "*");
    });
});

var app = builder.Build();
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}

// use the capability
app.UseCors(MyAllowSpecificOrigins);

//app.MapGet("/", () => "Hello World!");

app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync()); // すべてのピザを取得

app.MapGet("/pizzas/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id)); // idに紐づくピザを取得

app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) => {
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
 }); // 新しいピザを追加

app.MapPut("/pizzas/{id}", async (PizzaDb db, Pizza updatepizza, int id) => {
    var pizza = await db.Pizzas.FindAsync(id);
    if(pizza is null) return Results.NotFound();
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
}); // ピザを更新

app.MapDelete("/pizzas/{id}", async (PizzaDb db, int id) => {
    var pizza = await db.Pizzas.FindAsync(id);
    if(pizza is null) return Results.NotFound();
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
}); // idに紐づくピザを削除


app.Run();
