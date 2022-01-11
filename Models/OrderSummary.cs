namespace Models;

public class OrderSummary {
    public int OrderID {get;set;}
    public string Username {get;set;}
    public int StoreID {get;set;}
    public int CardID {get;set;}
    public int Quantity {get;set;}
    public DateOnly OrderDate {get;set;}
    public List<ShoppingCart> ShoppingCarts {get;set;}
    public decimal TotalPrice {get;set;}
}