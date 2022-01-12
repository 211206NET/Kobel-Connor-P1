namespace Models;

public class StoreOrder {
    public int OrderNumber {get;set;}
    public int StoreID {get;set;}
    public int CardID {get;set;}
    public int Quantity {get;set;}
    public string Date {get;set;}
    public decimal TotalPrice {get;set;}
}