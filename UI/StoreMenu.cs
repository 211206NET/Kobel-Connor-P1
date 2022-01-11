using BL;

using Models;
namespace UI;

public class StoreMenu {
    private IBL _bl;
    public StoreMenu(IBL bl)
    {
        _bl = bl;
    }
    public void Start(int storeID, string s) {
        List<PokemonCard> pokemonCards = _bl.GetStoreCards(storeID);

        int counter = 0;
        string input;
        string quantity;
        bool flag = false;

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
            Console.WriteLine("\nSelect a card to add to your shopping cart or type 'back' to return to main menu");
            input = Console.ReadLine();

            while (!flag) {
                try {
                    if (input == "back") {
                        flag = true;
                    } else if ((Convert.ToInt32(input)) > 0 && (Convert.ToInt32(input)) <= counter){
                        Console.WriteLine("How many would you like to add?");
                        quantity = Console.ReadLine();
                        while (Convert.ToInt32(quantity) <= 0 || Convert.ToInt32(quantity) > pokemonCards[Convert.ToInt32(input) - 1].Quantity) {
                            Console.WriteLine("\nThere are only " + pokemonCards[Convert.ToInt32(input) - 1].Quantity + " of the selected card available, try again.");
                            quantity = Console.ReadLine();
                        }
                        if (!_bl.CardAlreadyInShoppingCart(pokemonCards[Convert.ToInt32(input) - 1])) {
                            _bl.AddCardToShoppingCart(pokemonCards[Convert.ToInt32(input) - 1], Convert.ToInt32(quantity), s);
                            Console.WriteLine("\nCards added!");
                        } else {
                            Console.WriteLine("\nThis card is already in your shopping cart."); 
                        }
                        flag = true;
                    }
                    else {
                        Console.WriteLine("\nInvalid input. Enter either 'back', or a card's number.");
                        input = Console.ReadLine();
                    }
                }
                catch {
                    Console.WriteLine("\nInvalid input. Enter either 'back', or a card's number.");
                    input = Console.ReadLine();
                }
            }
        }
    }
}