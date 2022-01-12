using UI;
using DL;
using BL;
namespace UI;

public class MyStore {
    private IBL _bl;
    public MyStore(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        string input;
        bool flag = false;

        if (!_bl.MyStoreExist(s)) {
            Console.WriteLine("\nYou don't have a store set up yet. Would you like to do that now? (y/n)");
            input = Console.ReadLine();
            while (input != "y" && input != "n") {
                Console.WriteLine("\nPlease enter 'y' or 'n'");
                input = Console.ReadLine();
            }
            if (input == "y") {
                Console.WriteLine("What city is your store located?");
                string city = Console.ReadLine();
                Console.WriteLine("What state is exyour store located? (Please use 2 character state abreviation)");
                string state = Console.ReadLine();
                while (state.Length > 2) {
                    Console.WriteLine("State is too long, please use 2 character state abreviation");
                    state = Console.ReadLine();
                }
                _bl.CreateYourStore(s, city, state);
                Console.WriteLine("\nStore created!");
            } 
        } else {
            Console.WriteLine("\n[1] View store inventory");
            Console.WriteLine("[2] Add card(s)");
            Console.WriteLine("[3] View your stores order history");
            input = Console.ReadLine();

            while (!flag) {
                if (input == "1") {
                    GetMyStoreFront getMyStoreFront = new GetMyStoreFront(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                    getMyStoreFront.Start(s);
                    Console.WriteLine("\n[1] View store inventory");
                    Console.WriteLine("[2] Add card(s)");
                    Console.WriteLine("[3] View your stores order history");
                    Console.WriteLine("Type 'back' to go back to the main menu");
                    input = Console.ReadLine();
                }
                else if (input == "2") {
                    AddCards addCards = new AddCards(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                    addCards.Start(s);
                    Console.WriteLine("\n[1] View store inventory");
                    Console.WriteLine("[2] Add card(s)");
                    Console.WriteLine("[3] View your stores order history");
                    Console.WriteLine("Type 'back' to go back to the main menu");
                    input = Console.ReadLine();
                }
                else if (input == "3") {
                    Console.WriteLine("\n[1] View store inventory");
                    Console.WriteLine("[2] Add card(s)");
                    Console.WriteLine("[4] View your stores order history");
                    Console.WriteLine("Type 'back' to go back to the main menu");
                    input = Console.ReadLine();
                }
                else if (input == "back") {
                    flag = true;
                }
                else {
                    Console.WriteLine("\n[1] View store inventory");
                    Console.WriteLine("[2] Add card(s)");
                    Console.WriteLine("[4] View your stores order history");
                    Console.WriteLine("Type 'back' to go back to the main menu");
                    input = Console.ReadLine();
                }
            }
            
        }
    }
}