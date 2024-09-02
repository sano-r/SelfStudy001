namespace PizzaStore.DB;

public record Pizza
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class PizzaDb
{
    private static List<Pizza> _pizzas = new List<Pizza>(){
        new Pizza{ Id=1, Name="Montemagno, Pizza shaped like a great mountain" },
        new Pizza{ Id=2, Name="The Galloway, Pizza shaped like a submarine, silent but deadly"},
        new Pizza{ Id=3, Name="The Noring, Pizza shaped like a Viking helmet, where's the mead"}
    };

/// <summary>
/// 全部検索
/// </summary>
/// <returns></returns>
    public static List<Pizza> GetPizzas(){
        return _pizzas;
    }

/// <summary>
/// idで検索
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
    public static Pizza ? GetPizza(int id){
        return _pizzas.SingleOrDefault(pizza => pizza.Id == id);
    }

/// <summary>
/// insert Pizza
/// </summary>
/// <param name="pizza"></param>
/// <returns></returns>
    public static Pizza CreatePizza(Pizza pizza){
        _pizzas.Add(pizza);
        return pizza;
    }

/// <summary>
/// 更新 Pizza
/// 若干わかりづらい
/// </summary>
/// <param name="update"></param>
/// <returns></returns>
    public static Pizza UpdatePizza(Pizza update){
        _pizzas = _pizzas.Select(pizza => {
            if(pizza.Id == update.Id){
                pizza.Name = update.Name; // Idが一致していたら更新
            }
            return pizza;
        }).ToList(); // コンストラクタを更新したリストで新しいメモリ空間を確保
        return update;
    }

    public static void RemovePizza(int id){
        _pizzas = _pizzas.FindAll(pizza => pizza.Id != id).ToList();
    }
}