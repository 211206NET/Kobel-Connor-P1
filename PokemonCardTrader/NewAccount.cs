namespace UI;

public class NewAccount {
    public void Start() {
        Console.WriteLine("\nPlease enter a new and unique username");
        string username = Console.ReadLine();
        // DL to make sure username doesn't exist

        Console.WriteLine("Please enter a new password");
        string password = Console.ReadLine();
        Console.WriteLine("Please verify your password by entering it again");
        string passwordCheck = Console.ReadLine();

        while (password != passwordCheck) {
            Console.WriteLine("\nYour passwords did not match");
            Console.WriteLine("Please enter a new password");
            password = Console.ReadLine();
            Console.WriteLine("Please verify your password by entering it again");
            passwordCheck = Console.ReadLine();
        }
        Console.WriteLine("Your password is " + password);
        // DL to add new username and password
    }
}