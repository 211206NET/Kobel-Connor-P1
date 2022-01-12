using BL;
using DL;
using Models;
namespace UI;

public class YourCart {
    private IBL _bl;
    public YourCart(IBL bl)
    {
        _bl = bl;
    }

    public void Start(string s) {
        List<PokemonCard> cards = _bl.ShowYourCart(s);
        ShoppingCart cart = new ShoppingCart();
        int counter = 0;
        decimal runningTotal = 0;

        Console.WriteLine("\n---------------------" + s + "'s shopping cart---------------------\n");
        foreach(PokemonCard c in cards) {
            Console.WriteLine("[" + ++counter + "]-------");
            Console.WriteLine(c.CardName);
            Console.WriteLine(c.CardSet);
            Console.WriteLine(c.ConditionTitle);
            Console.WriteLine(c.FoilTitle);
            Console.WriteLine("$" + c.Price);
            Console.WriteLine("Quantity: " + c.Quantity + "\n\n");
            runningTotal += (c.Price * c.Quantity);
        }
        Console.WriteLine("Running Total: " + runningTotal + "\n");

        string input;
        bool flag = false;

        Console.WriteLine("Select a card to remove or type 'checkout' to checkout (type 'back' to return to main menu)");
        input = Console.ReadLine();
        while (!flag) {
            try {
                if (input == "back") {
                    flag = true;
                } else if (input == "checkout") {
                    // checkout
                    _bl.Checkout(s, cards, runningTotal);                    
                    Console.WriteLine("\nChecked out.");
                    flag = true;
                } else if ((Convert.ToInt32(input)) > 0 && (Convert.ToInt32(input)) <= counter){
                    _bl.DeleteCardFromShoppingCart(cards[Convert.ToInt32(input) - 1], s);
                    Console.WriteLine("\nCard(s) deleted!");
                    flag = true;
                }
                else {
                    Console.WriteLine("\nInvalid input. Enter either 'checkout', 'back', or a card's number.");
                    Console.WriteLine("Select a card to remove or type 'checkout' to checkout (type 'back' to return to main menu)");
                    input = Console.ReadLine();
                }
            }
            catch {
                Console.WriteLine("\nInvalid input. Enter either 'checkout', 'back', or a card's number.");
                input = Console.ReadLine();
            }
        }
    }
}