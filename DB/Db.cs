using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.DB;
public class PizzaDb : DbContext
{
    public DbSet<Pizza> Pizzas{get; set;} = null!;

    //コンストラクタは必要。
    public PizzaDb(DbContextOptions options) : base(options){}

    //SqlServerにつなぐなら下記
    //証明書でエラーが出るのでTrustedServerCertificate=Trueが必要
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=Sample;uid=sa;pwd=rsanoPassword0716;TrustServerCertificate=True;");
    }

}