namespace Models;

public class Customer {
    public string Username {get;set;}
    public string Email {get;set;}
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Password {get;set;}
    public List<OrderSummary> Orders {get;set;}
}