using UI;
using DL;
using BL;
using Models;
namespace UI;

public class UserOrders {
    private IBL _bl;
    public UserOrders(IBL bl)
    {
        _bl = bl;
    }
    public void Start(string s) {
        int counter = 0;
        List<StoreOrder> orders = _bl.UserOrderHistory(s);
        Console.WriteLine("\n-------------------" + s + "'s Orders-------------------\n");
        foreach (StoreOrder order in orders) {
            Console.WriteLine("[" + ++counter + "]-------");
            Console.WriteLine("Order number: " + order.OrderNumber);
            Console.WriteLine("Store ID: " + order.StoreID);
            Console.WriteLine("Card ID: " + order.CardID);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Order date: " + order.Date);
            Console.WriteLine("Total price: $" + order.TotalPrice + "\n");
        }
    }
}