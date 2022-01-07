using BL;
using BC = BCrypt.Net.BCrypt;
namespace UI;

public class NewAccount {
    private IBL _bl;
    public NewAccount(IBL bl)
    {
        _bl = bl;
    }
    public void Start() {
        Console.WriteLine("\nPlease enter a new and unique username");
        string username = Console.ReadLine();
        // DL to make sure username doesn't exist
        while (_bl.CheckUsernameExists(username)) {
            Console.WriteLine("\nThis username already exists. Try a different one!");
            username = Console.ReadLine();
        }

        Console.WriteLine("Please enter a new password");
        string password = BC.HashPassword(Console.ReadLine(), BC.GenerateSalt(12));
        Console.WriteLine(password);
        Console.WriteLine("Please verify your password by entering it again");
        bool passwordCheck = BC.Verify(Console.ReadLine(), password);
        Console.WriteLine(passwordCheck);

        while (!passwordCheck) {
            Console.WriteLine("\nYour passwords did not match");
            Console.WriteLine("Please enter a new password");
            password = BC.HashPassword(Console.ReadLine(), BC.GenerateSalt(12));
            Console.WriteLine("Please verify your password by entering it again");
            passwordCheck = BC.Verify(Console.ReadLine(), password);
        }
        // DL to add new username and password
    }
}