using Models;
using DL;

namespace BL;
public class CardTrader : IBL {
    private IRepo _dl;

    public CardTrader(IRepo repo) {
        _dl = repo;
    }

    public void GetAllPokemonCards(string username) {
        List<PokemonCard> pokemonCards = _dl.GetAllPokemonCards();

        int counter = 0;
        string input;
        string quantity;

        if (pokemonCards.Count == 0) {
            Console.WriteLine("There are currently no Pokemon cards for sale.");
        } else {
            Console.WriteLine();
            foreach (PokemonCard card in pokemonCards) {
                Console.WriteLine("[" + ++counter + "]-------");
                Console.WriteLine(card.CardName);
                Console.WriteLine(card.CardSet);
                Console.WriteLine(card.ConditionTitle);
                Console.WriteLine(card.FoilTitle);
                Console.WriteLine("$" + card.Price);
                Console.WriteLine("Quantity availible: " + card.Quantity + "\n");
            }
            Console.WriteLine("\nSelect a card to add to your shopping cart less than " + counter);
            input = Console.ReadLine();
            while ((Convert.ToInt32(input) - 1) <= 0 || (Convert.ToInt32(input) - 1) > counter) {
                Console.WriteLine("\nNot a viable selection.");
                Console.WriteLine("Select a card to add to your shopping cart");
                input = Console.ReadLine();
            }
            Console.WriteLine(pokemonCards[Convert.ToInt32(input) - 1].CardName);
            Console.WriteLine("How many would you like to add?");
            quantity = Console.ReadLine();
            while (Convert.ToInt32(quantity) <= 0 || Convert.ToInt32(quantity) > pokemonCards[Convert.ToInt32(input) - 1].Quantity) {
                Console.WriteLine("\nThere are only " + pokemonCards[Convert.ToInt32(input) - 1].Quantity + " of the selected card available, try again.");
                quantity = Console.ReadLine();
            }
            if (!_dl.CardAlreadyInShoppingCart(pokemonCards[Convert.ToInt32(input) - 1])) {
                _dl.AddCardToShoppingCart(pokemonCards[Convert.ToInt32(input) - 1], Convert.ToInt32(quantity), username);
                Console.WriteLine("\nCards added!");
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