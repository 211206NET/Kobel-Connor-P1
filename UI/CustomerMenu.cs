using BL;
using DL;
namespace UI;

public class CustomerMenu {
    private IBL _bl;
    public CustomerMenu(IBL bl)
    {
        _bl = bl;
    }

    public void Start(string s) {
        bool flag = false;

        Console.WriteLine("\nPlease select an option");
        Console.WriteLine("[0] Inventory of all stores");
        Console.WriteLine("[1] Availible stores");
        Console.WriteLine("[2] Manage your own store");
        Console.WriteLine("[3] Shopping cart");
        Console.WriteLine("[4] View your order history");
        //Console.WriteLine("[4] View your profile information");
        Console.WriteLine("Type 'exit' to exit");
        string input = Console.ReadLine();

        while (!flag){
            if (input == "0") {
                AllCards allCards = new AllCards(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                allCards.Start(s);
                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] Inventory of all stores");
                Console.WriteLine("[1] Availible stores");
                Console.WriteLine("[2] Manage your own store");
                Console.WriteLine("[3] Shopping cart");
                Console.WriteLine("[4] View your order history");
                //Console.WriteLine("[4] View your profile information");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            } 
            else if (input == "1") {
                // DL Display all storefronts
                AllStores allStores = new AllStores(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                allStores.Start(s);
                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] Inventory of all stores");
                Console.WriteLine("[1] Availible stores");
                Console.WriteLine("[2] Manage your own store");
                Console.WriteLine("[3] Shopping cart");
                Console.WriteLine("[4] View your order history");
                //Console.WriteLine("[4] View your profile information");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
            else if (input == "2") {
                // Storefront menu

                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] Inventory of all stores");
                Console.WriteLine("[1] Availible stores");
                Console.WriteLine("[2] Manage your own store");
                Console.WriteLine("[3] Shopping cart");
                Console.WriteLine("[4] View your order history");
                //Console.WriteLine("[4] View your profile information");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
            else if (input == "3") {
                // Shopping cart
                YourCart yourCart = new YourCart(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                yourCart.Start(s);
                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] Inventory of all stores");
                Console.WriteLine("[1] Availible stores");
                Console.WriteLine("[2] Manage your own store");
                Console.WriteLine("[3] Shopping cart");
                Console.WriteLine("[4] View your order history");
                //Console.WriteLine("[4] View your profile information");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
            else if (input == "4") {
                // Display customer order history


                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] Inventory of all stores");
                Console.WriteLine("[1] Availible stores");
                Console.WriteLine("[2] Manage your own store");
                Console.WriteLine("[3] Shopping cart");
                Console.WriteLine("[4] View your order history");
                //Console.WriteLine("[4] View your profile information");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
            else if (input == "exit") {
                Console.WriteLine("Thanks for shopping! Goodbye!");
                flag = true;
            }
            else {
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("[0] View inventory of all stores");
                Console.WriteLine("[1] View availible stores");
                Console.WriteLine("[2] Manage your own store");
                Console.WriteLine("[3] View your order history");
                //Console.WriteLine("[4] View your profile information");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
        }
    }
}