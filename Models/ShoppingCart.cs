namespace Models;

public class ShoppingCart {
    public string Username {get;set;}
    public List<PokemonCard> Card {get;set;}
    public decimal RunningTotal {get;set;}
}