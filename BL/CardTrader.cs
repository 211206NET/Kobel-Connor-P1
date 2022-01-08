using Models;
using DL;

namespace BL;
public class CardTrader : IBL {
    private IRepo _dl;

    public CardTrader(IRepo repo) {
        _dl = repo;
    }

    public void GetAllPokemonCards() {
        List<PokemonCard> pokemonCards = _dl.GetAllPokemonCards();

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

    public bool CheckUsernameExists(string s) {
        if (_dl.CheckDbForUsername(s)) {
            return true;
        }
        return false;
    }

    public void AddNewUser(Customer c) {
        _dl.AddNewUserDB(c);
    }

    public bool Login(string u, string p) {
        if (_dl.LoginDB(u, p)) {
            return true;
        }
        return false;
    }
}