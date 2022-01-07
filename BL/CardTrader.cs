using Models;
using DL;

namespace BL;
public class CardTrader : IBL {
    private IRepo _d1;

    public CardTrader(IRepo repo) {
        _d1 = repo;
    }

    public void GetAllPokemonCards() {
        List<PokemonCard> pokemonCards = _d1.GetAllPokemonCards();

        if (pokemonCards.Count == 0) {
            Console.WriteLine("There are currently no Pokemon cards for sale.");
        } else {
            Console.WriteLine();
            foreach (PokemonCard card in pokemonCards) {
                Console.WriteLine(card.CardName);
                Console.WriteLine(card.CardSet);
                Console.WriteLine(card.ConditionTitle);
                Console.WriteLine(card.FoilTitle);
                Console.WriteLine(card.Price + "\n");
            }
        }
    }
}