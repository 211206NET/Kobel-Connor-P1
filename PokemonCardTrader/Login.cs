using UI;
namespace UI;

public class Login {
    public void Start() {
        Console.WriteLine("\nPlease enter your username");
        string username = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();

        // call DL to verify stuff
        Console.WriteLine("Account logged in!");
        CustomerMenu customerMenu = new CustomerMenu();
        customerMenu.Start();
    }
}