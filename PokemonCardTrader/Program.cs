Console.WriteLine("Welcome to Pokemon Card Seller!\n");

bool flag = false;

Console.WriteLine("Please select an option");
Console.WriteLine("[0] I already have an account - Login");
Console.WriteLine("[1] I don't have an account - Create new account");
Console.WriteLine("Type 'exit' to exit");
string input = Console.ReadLine();

while (!flag){
    if (input == "0") {
        Console.WriteLine("Account logged in!");
        flag = true;
    } 
    else if (input == "1") {
        Console.WriteLine("New account created!");
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
        input = Console.ReadLine();
    }
}