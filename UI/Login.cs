using UI;
using DL;
using BL;
namespace UI;

public class Login {
    private IBL _bl;
    public Login(IBL bl)
    {
        _bl = bl;
    }
    public void Start() {
        string input;
        Console.WriteLine("\nPlease enter your username");
        string username = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();

        // call DL to verify stuff
        if (_bl.Login(username, password)) {
            Console.WriteLine("\nAccount logged in!");
            CustomerMenu customerMenu = new CustomerMenu(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
            customerMenu.Start(username);
        } else {
            Console.WriteLine("\nYou entered incorrect credentials, Would you like to try again?\n('y' to try again, any other character to go back)");
            input = Console.ReadLine();
            if (input.ToLower() == "y") {
                Login login = new Login(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                login.Start();
            } else {
                LoginMenu loginMenu = new LoginMenu();
                loginMenu.Start();
            }
        }
    }
}