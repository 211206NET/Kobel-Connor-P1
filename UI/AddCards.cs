using UI;
using DL;
using BL;
using Models;
namespace UI;

public class AddCards {
    private IBL _bl;
    public AddCards(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        Console.WriteLine("\nEnter the card name?");
        string cardName = Console.ReadLine();
        Console.WriteLine("\nEnter the card's set?");
        string cardSet = Console.ReadLine();

        Console.WriteLine("\nWhat is the cards's condition?");
        Console.WriteLine("[0] Mint");
        Console.WriteLine("[1] Near Mint");
        Console.WriteLine("[2] Lightly Played");
        Console.WriteLine("[3] Moderately Played");
        Console.WriteLine("[4] Heavily Played");
        Console.WriteLine("[5] Damaged");
        int conditionID = Convert.ToInt32(Console.ReadLine());
        // string conditionTitle;
        // if (conditionID == 0) {
        //     conditionTitle = "Mint";
        // } else if (conditionID == 1) {
        //     conditionTitle = "Near Mint";
        // } else if (conditionID == 2) {
        //     conditionTitle = "Lightly Played";
        // } else if (conditionID == 3) {
        //     conditionTitle = "Moderately Played";
        // } else if (conditionID == 4) {
        //     conditionTitle = "Heavily Played";
        // } else if (conditionID == 5) {
        //     conditionTitle = "Damaged";
        // }

        Console.WriteLine("\nWhat is the cards's foil type?");
        Console.WriteLine("[0] Normal Print");
        Console.WriteLine("[1] Rare Holo");
        Console.WriteLine("[2] Reverse Holo");
        Console.WriteLine("[3] Ultra Rare");
        Console.WriteLine("[4] Secret Rare");
        int foilID = Convert.ToInt32(Console.ReadLine());
        // string foilTitle;
        // if (foilID == 0) {
        //     foilTitle = "Mint";
        // } else if (foilID == 1) {
        //     foilTitle = "Near Mint";
        // } else if (foilID == 2) {
        //     foilTitle = "Lightly Played";
        // } else if (foilID == 3) {
        //     foilTitle = "Moderately Played";
        // } else if (foilID == 4) {
        //     foilTitle = "Heavily Played";
        // }

        Console.WriteLine("\nHow much are you going to sell the card for?");
        decimal price = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("\nHow many do you have to sell?");
        int quantity = Convert.ToInt32(Console.ReadLine());

        _bl.AddCardToStore(s, cardName, cardSet, conditionID, foilID, price, quantity);
        Console.WriteLine("\nCards added!");
    }
}