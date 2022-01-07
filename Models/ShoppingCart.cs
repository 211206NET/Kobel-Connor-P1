namespace Models;

public class ShoppingCart {
    public string UserName {get;set;}
    public List<StoreOrder> StoreOrders {get;set;}
    public decimal RunningTotal {get;set;}
    public decimal CalculateRunningTotal() {
        decimal runningTotal = 0;
        if(this.StoreOrders?.Count > 0) {
            foreach(StoreOrder storeorder in this.StoreOrders) {
                runningTotal += storeorder.CalculateOrderPrice();
            }
        }
        this.RunningTotal = runningTotal;
        return runningTotal;
    }
}