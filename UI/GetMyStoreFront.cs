using UI;
using DL;
using BL;
using Models;
namespace UI;

public class GetMyStoreFront {
    private IBL _bl;
    public GetMyStoreFront(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        List<PokemonCard> cards = _bl.MyStoreCards(s);

        int counter = 0;

        if (cards.Count == 0) {
            Console.WriteLine("There are currently no Pokemon cards for sale.");
        } else {
            Console.WriteLine();
            foreach (PokemonCard card in cards) {
                Console.WriteLine("[" + ++counter + "]-------");
                Console.WriteLine(card.CardName);
                Console.WriteLine(card.CardSet);
                Console.WriteLine(card.ConditionTitle);
                Console.WriteLine(card.FoilTitle);
                Console.WriteLine("$" + card.Price);
                Console.WriteLine("Quantity availible: " + card.Quantity + "\n");
            }
        }
        Console.WriteLine("\nSelect a card(s) to remove from your store or type 'back' to return to main menu");
        string input;
        bool flag = false;
        input = Console.ReadLine();
        Console.WriteLine(input);

        while (!flag) {
            try {
                if (input == "back") {
                    flag = true;
                } else if ((Convert.ToInt32(input)) > 0 && (Convert.ToInt32(input)) <= counter){
                    _bl.DeleteCardFromMyStore(cards[Convert.ToInt32(input) - 1], s);
                    Console.WriteLine("\nCard(s) deleted!");
                    flag = true;
                }
                else {
                    Console.WriteLine("\nInvalid input. Enter either 'back' or a card's number.");
                    input = Console.ReadLine();
                }
            }
            catch {
                Console.WriteLine("\n2Invalid input. Enter either 'back', or a card's number.");
                input = Console.ReadLine();
            }
        }
    }
}