using BL;

using Models;
namespace UI;

public class AllCards {
    private IBL _bl;
    public AllCards(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        // DL Display all cards from all stores
        List<PokemonCard> pokemonCards = _bl.GetAllPokemonCards();

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
            Console.WriteLine("\nSelect a card to add to your shopping cart less than " + counter);
            Console.WriteLine("Or you can type 'back' to return to main menu.");
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
                        Console.WriteLine("\nInvalid input. Enter either 'back' or a card's number.");
                        Console.WriteLine("Select a card to remove or type 'checkout' to checkout (type 'back' to return to main menu)");
                        input = Console.ReadLine();
                    }
                }
                catch {
                    Console.WriteLine("\nInvalid input. Enter either 'back' or a card's number.");
                    input = Console.ReadLine();
                }
            }
        }
    }
}