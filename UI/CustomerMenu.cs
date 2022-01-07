using BL;
namespace UI;

public class CustomerMenu {

    private IBL _bl;
    public CustomerMenu(IBL bl)
    {
        _bl = bl;
    }

    public void Start() {
        bool flag = false;

        Console.WriteLine("\nPlease select an option");
        Console.WriteLine("[0] View inventory of all stores");
        Console.WriteLine("[1] View inventory of a specific store");
        Console.WriteLine("[2] View your order history");
        Console.WriteLine("Type 'exit' to exit");
        string input = Console.ReadLine();

        while (!flag){
            if (input == "0") {
                // DL Display all cards from all stores
                _bl.GetAllPokemonCards();

                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] View inventory of all stores");
                Console.WriteLine("[1] View inventory of a specific store");
                Console.WriteLine("[2] View your order history");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            } 
            else if (input == "1") {
                // DL Display all storefronts
                Console.WriteLine("Display all storefronts");
                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] View inventory of all stores");
                Console.WriteLine("[1] View inventory of a specific store");
                Console.WriteLine("[2] View your order history");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
            else if (input == "2") {
                // Display all customer orders
                Console.WriteLine("Display all this customer's orders");
                Console.WriteLine("\nAnything else?");
                Console.WriteLine("[0] View inventory of all stores");
                Console.WriteLine("[1] View inventory of a specific store");
                Console.WriteLine("[2] View your order history");
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
                Console.WriteLine("[1] View inventory of a specific store");
                Console.WriteLine("[2] View your order history");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
        }
    }
}