namespace Models;

public class StoreOrder {
    public int StoreID {get;set;}
    public List<PokemonCard> PokemonCards {get;set;}
    public decimal OrderPrice {get;set;}
    public decimal CalculateOrderPrice() {
        decimal orderPrice = 0;
        if(this.PokemonCards?.Count > 0) {
            foreach(PokemonCard pokecard in this.PokemonCards) {
                orderPrice += pokecard.Price;
            }
        }
        this.OrderPrice = orderPrice;
        return orderPrice;
    }
}