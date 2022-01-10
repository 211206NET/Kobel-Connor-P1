using BL;
namespace UI;

public class AllCards {
    private IBL _bl;
    public AllCards(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        // DL Display all cards from all stores
        _bl.GetAllPokemonCards(s);
    }
}