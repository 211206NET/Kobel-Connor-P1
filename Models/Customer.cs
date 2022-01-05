namespace Models;

public class Customer {
    public string UserName {get;set;}
    public string Email {get;set;}
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Password {get;set;}
    public List<Orders> Orders {get;set;}
}