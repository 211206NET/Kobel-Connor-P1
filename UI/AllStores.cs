using BL;
using DL;
using Models;
namespace UI;

public class AllStores {
    private IBL _bl;
    public AllStores(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        // DL Display all cards from all stores
        List<StoreFront> allStores = _bl.GetAllStoreFronts();

        int counter = 0;
        int quantity = 0;
        string input;
        int storeID;

        if (allStores.Count == 0) {
            Console.WriteLine("There are currently no stores available.");
        } else {
            Console.WriteLine();
            foreach (StoreFront store in allStores) {
                Console.WriteLine("[" + ++counter + "]-------");
                Console.WriteLine(store.City);
                Console.WriteLine(store.State);
                Console.WriteLine(store.Username);
                Console.WriteLine("Total cards availible: " + _bl.CardsAvailableForIndividualStore(store) + "\n");
            }
        }

        Console.WriteLine("Select a store to view its inventory (type 'back' to return to main menu)");
        input = Console.ReadLine();
        while((Convert.ToInt32(input)) <= 0 || (Convert.ToInt32(input)) > counter) {
            Console.WriteLine("\nNot a viable selection.");
            Console.WriteLine("Select a store to view its inventory or type 'back'");
            input = Console.ReadLine(); 
        }
        input = Convert.ToString(input);
        if (input == "back") {
            CustomerMenu customerMenu = new CustomerMenu(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
            customerMenu.Start(s);
        } else {
            storeID = allStores[Convert.ToInt32(input) - 1].StoreID;
            StoreMenu storeMenu = new StoreMenu(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
            storeMenu.Start(storeID, s);
        }
    }
}