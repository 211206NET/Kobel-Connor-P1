using UI;
using BL;
using DL;
namespace UI;

public class LoginMenu {
    public void Start() {
        bool flag = false;

        Console.WriteLine("\nPlease select an option");
        Console.WriteLine("[0] I already have an account - Login");
        Console.WriteLine("[1] I don't have an account - Create new account");
        Console.WriteLine("Type 'exit' to exit");
        string input = Console.ReadLine();

        while (!flag){
            if (input == "0") {
                Login login = new Login(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                login.Start();
                flag = true;
            } 
            else if (input == "1") {
                NewAccount newAccount = new NewAccount(new CardTrader(new DBRepo(File.ReadAllText("connectionstring.txt"))));
                newAccount.Start();
                flag = true;
            }
            else if (input == "exit") {
                Console.WriteLine("Thanks for shopping! Goodbye!");
                flag = true;
            }
            else {
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("[0] I already have an account - Login");
                Console.WriteLine("[1] I don't have an account - Create new account");
                Console.WriteLine("Type 'exit' to exit");
                input = Console.ReadLine();
            }
        }
    }
}