using BL;
using DL;
using BC = BCrypt.Net.BCrypt;
using Models;
namespace UI;

public class NewAccount {
    private IBL _bl;
    public NewAccount(IBL bl)
    {
        _bl = bl;
    }
    public void Start() {
        int salt = 12;
        Customer c = new Customer();
        Console.WriteLine("\nPlease enter a new and unique username");
        string username = Console.ReadLine();
        // DL to make sure username doesn't exist
        while (_bl.CheckUsernameExists(username)) {
            Console.WriteLine("\nThis username already exists. Try a different one!");
            username = Console.ReadLine();
        }
        c.Username = username;

        Console.WriteLine("Please enter a new password");
        string password = BC.HashPassword(Console.ReadLine(), BC.GenerateSalt());
        Console.WriteLine("Please verify your password by entering it again");
        bool passwordCheck = BC.Verify(Console.ReadLine(), password);

        while (!passwordCheck) {
            Console.WriteLine("\nYour passwords did not match");
            Console.WriteLine("Please enter a new password");
            password = BC.HashPassword(Console.ReadLine(), salt);
            Console.WriteLine("Please verify your password by entering it again");
            passwordCheck = BC.Verify(Console.ReadLine(), password);
        }
        c.Password = password;
        Console.WriteLine("What is your first name?");
        string firstName = Console.ReadLine();
        Console.WriteLine("What is your last name?");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter your email address");
        string email = Console.ReadLine();

        c.FirstName = firstName;
        c.LastName = lastName;
        c.Email = email;

        // DL to add new username and password
        _bl.AddNewUser(c);
        Console.WriteLine("\nNew account created!");
        CustomerMenu customerMenu = new CustomerMenu(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
        customerMenu.Start(username);
    }
}