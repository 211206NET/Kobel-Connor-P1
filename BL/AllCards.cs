using Models;

namespace BL;
public class AllCards : IBL {
    private IRepo _d1;

    public AllCards(IRepo repo) {
        _d1 = repo;
    }

    public List<PokemonCard> GetAllPokemonCards() {
        return _d1.GetAllPokemonCards();
    }
}