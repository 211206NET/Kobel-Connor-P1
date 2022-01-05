namespace Models;

public class PokemonCard {
    public int StoreID {get;set;}
    public int CardID {get;set;}
    public string CardName {get;set;}
    public string CardSet {get;set;}
    public int ConditionID {get;set;}
    public int FoilID {get;set;}
    public decimal Price {get;set;}
}