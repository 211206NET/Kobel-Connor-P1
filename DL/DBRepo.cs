using Microsoft.Data.SqlClient;
using System.Data;
using Models;
using BC = BCrypt.Net.BCrypt;
// using System;
// using System.Data;
// using System.Collections.Generic;

namespace DL;

public class DBRepo : IRepo {
    private string _connectionString;
    private SqlDataAdapter adapter = new SqlDataAdapter();
    public DBRepo(string connectionString) {
        _connectionString = connectionString;
    }

    public bool CheckDbForUsername(string s) {
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string checkForUsername = "SELECT COUNT (*) username FROM Customer WHERE username = @USERNAME";
                    adapter.InsertCommand = new SqlCommand(checkForUsername, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = s;

                    adapter.InsertCommand.Transaction = transaction;
                    int result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                    if (result != 0)
                        return true;
                    else return false;
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return false;
    }

    public bool LoginDB(string u, string p) {
        int count = 0;
        if (CheckDbForUsername(u))
            count++;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string getPass = "SELECT hashPassword FROM Customer WHERE username = @USERNAME";
                    adapter.InsertCommand = new SqlCommand(getPass, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = u;

                    adapter.InsertCommand.Transaction = transaction;
                    string result = Convert.ToString(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                    if (count == 1) {
                        if (BC.Verify(p, result))
                            return true;
                        else return false;
                    } else return false;
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return false;
    }

    public void AddNewUserDB(Customer c) {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "INSERT INTO Customer (username, email, firstName, lastName, hashPassword) VALUES (@USERNAME, @EMAIL, @FIRSTNAME, @LASTNAME, @HASHPASSWORD)";
        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        addNew.Parameters.AddWithValue("@USERNAME", c.Username);
        addNew.Parameters.AddWithValue("@EMAIL", c.Email);
        addNew.Parameters.AddWithValue("@FIRSTNAME", c.FirstName);
        addNew.Parameters.AddWithValue("@LASTNAME", c.LastName);
        addNew.Parameters.AddWithValue("@HASHPASSWORD", c.Password);

        addNew.ExecuteNonQuery();
        connection.Close();
    }

    public List<PokemonCard> GetAllPokemonCardsDB() {
        List<PokemonCard> allPokemonCards = new List<PokemonCard>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string cardsSelect = "SELECT * FROM PokemonCard INNER JOIN Inventory ON Inventory.CardID = PokemonCard.CardID INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID ORDER BY cardName";

        DataSet CardSet = new DataSet();

        using SqlDataAdapter cardAdapter = new SqlDataAdapter(cardsSelect, connection);

        cardAdapter.Fill(CardSet, "PokemonCard");

        DataTable? PokemonCardTable = CardSet.Tables["PokemonCard"];

        if (PokemonCardTable != null) {
            foreach(DataRow row in PokemonCardTable.Rows) {
                PokemonCard card = new PokemonCard();
                card.StoreID = Convert.ToInt32(row["storeID"]);
                card.CardID = Convert.ToInt32(row["cardID"]);
                card.CardName = Convert.ToString(row["cardName"]);
                card.CardSet = Convert.ToString(row["cardSet"]);
                card.ConditionTitle = Convert.ToString(row["conditionTitle"]);
                card.FoilTitle = Convert.ToString(row["foilTitle"]);
                card.Price = Convert.ToDecimal(row["price"]);
                card.Quantity = Convert.ToInt32(row["quantity"]);

                allPokemonCards.Add(card);
            }
        }

        return allPokemonCards;
    }

    public bool CardAlreadyInShoppingCartDB(PokemonCard card) {
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string checkForCard = "SELECT COUNT (*) cardID FROM ShoppingCart WHERE cardID = @CARDID";
                    adapter.InsertCommand = new SqlCommand(checkForCard, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@CARDID", SqlDbType.VarChar).Value = card.CardID;

                    adapter.InsertCommand.Transaction = transaction;
                    int result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                    if (result != 0)
                        return true;
                    else return false;
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return false;
    }

    public void AddCardToShoppingCartDB(PokemonCard card, int quantity, string username) {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "INSERT INTO ShoppingCart (username, storeID, cardID, quantity, individualPrice) VALUES (@USERNAME, @STOREID, @CARDID, @QUANTITY, @INDIVIDUALPRICE)";
        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        addNew.Parameters.AddWithValue("@USERNAME", username);
        addNew.Parameters.AddWithValue("@STOREID", card.StoreID);
        addNew.Parameters.AddWithValue("@CARDID", card.CardID);
        addNew.Parameters.AddWithValue("@QUANTITY", quantity);
        addNew.Parameters.AddWithValue("@INDIVIDUALPRICE", quantity * card.Price);

        addNew.ExecuteNonQuery();
        connection.Close();
    }

    public int CardsAvailableForIndividualStoreDB(StoreFront store) {
        int total = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string getQuantity = "SELECT SUM(quantity) FROM Inventory WHERE storeID = @STOREID";
                    adapter.InsertCommand = new SqlCommand(getQuantity, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@STOREID", SqlDbType.VarChar).Value = store.StoreID;

                    adapter.InsertCommand.Transaction = transaction;
                    total = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return total;
    }
    
    public List<StoreFront> GetAllStoreFrontsDB() {
        List<StoreFront> allStores = new List<StoreFront>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string storesSelect = "SELECT * FROM StoreFront";

        DataSet StoreSet = new DataSet();

        using SqlDataAdapter storeAdapter = new SqlDataAdapter(storesSelect, connection);

        storeAdapter.Fill(StoreSet, "StoreFront");

        DataTable? StoreTables = StoreSet.Tables["StoreFront"];

        if (StoreTables != null) {
            foreach(DataRow row in StoreTables.Rows) {
                StoreFront store = new StoreFront();
                store.StoreID = Convert.ToInt32(row["storeID"]);
                store.Username = Convert.ToString(row["username"]);
                store.City = Convert.ToString(row["city"]);
                store.State = Convert.ToString(row["state"]);

                allStores.Add(store);
            }
        }

        return allStores;
    }

    public List<PokemonCard> GetStoreCardsDB(int storeID) {
        List<PokemonCard> allPokemonCards = new List<PokemonCard>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string cardsSelect = "SELECT * FROM PokemonCard INNER JOIN Inventory ON Inventory.CardID = PokemonCard.CardID INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID WHERE storeID = @STOREID ORDER BY cardName";
        /*
            "SELECT * FROM PokemonCard 
            INNER JOIN Inventory ON Inventory.CardID = PokemonCard.CardID 
            INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID 
            INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID 
            WHERE storeID = @STOREID 
            ORDER BY cardName"
        */
        DataSet CardSet = new DataSet();

        using SqlDataAdapter cardAdapter = new SqlDataAdapter(cardsSelect, connection);
        cardAdapter.SelectCommand.Parameters.AddWithValue("@STOREID", storeID);

        cardAdapter.Fill(CardSet, "PokemonCard");

        DataTable? PokemonCardTable = CardSet.Tables["PokemonCard"];

        if (PokemonCardTable != null) {
            foreach(DataRow row in PokemonCardTable.Rows) {
                PokemonCard card = new PokemonCard();
                card.StoreID = Convert.ToInt32(row["storeID"]);
                card.CardID = Convert.ToInt32(row["cardID"]);
                card.CardName = Convert.ToString(row["cardName"]);
                card.CardSet = Convert.ToString(row["cardSet"]);
                card.ConditionTitle = Convert.ToString(row["conditionTitle"]);
                card.FoilTitle = Convert.ToString(row["foilTitle"]);
                card.Price = Convert.ToDecimal(row["price"]);
                card.Quantity = Convert.ToInt32(row["quantity"]);

                allPokemonCards.Add(card);
            }
        }

        return allPokemonCards;
    }

    public List<PokemonCard> ShowYourCartDB(string s) {
        List<PokemonCard> cardCart = new List<PokemonCard>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string cartCmd = "SELECT * FROM ShoppingCart INNER JOIN Inventory ON Inventory.cardID = ShoppingCart.cardID INNER JOIN PokemonCard ON PokemonCard.cardID = Inventory.cardID INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID WHERE username = @USERNAME";
        
        DataSet CartSet = new DataSet();

        using SqlDataAdapter cartAdapter = new SqlDataAdapter(cartCmd, connection);
        cartAdapter.SelectCommand.Parameters.AddWithValue("@USERNAME", s);

        cartAdapter.Fill(CartSet, "ShoppingCart");

        DataTable? CartTable = CartSet.Tables["ShoppingCart"];

        if (CartTable != null) {
            foreach(DataRow row in CartTable.Rows) {
                PokemonCard card = new PokemonCard();
                card.StoreID = Convert.ToInt32(row["storeID"]);
                card.CardID = Convert.ToInt32(row["cardID"]);
                card.CardName = Convert.ToString(row["cardName"]);
                card.CardSet = Convert.ToString(row["cardSet"]);
                card.ConditionTitle = Convert.ToString(row["conditionTitle"]);
                card.FoilTitle = Convert.ToString(row["foilTitle"]);
                card.Price = Convert.ToDecimal(row["price"]);
                card.Quantity = Convert.ToInt32(row["quantity"]);
                cardCart.Add(card);
            }
        }
        return cardCart;
    }

    public void DeleteCardFromShoppingCartDB(PokemonCard card, string username) {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "DELETE FROM ShoppingCart WHERE username = @USERNAME AND cardID = @CARDID";
        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        addNew.Parameters.AddWithValue("@USERNAME", username);
        addNew.Parameters.AddWithValue("@CARDID", card.CardID);

        addNew.ExecuteNonQuery();
        connection.Close();
    }

    public decimal GetTotalPriceDB(string username) {
        decimal total = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string getTotal = "SELECT SUM(individualPrice) FROM ShoppingCart WHERE username = @USERNAME";
                    adapter.InsertCommand = new SqlCommand(getTotal, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = username;

                    adapter.InsertCommand.Transaction = transaction;
                    total = Convert.ToDecimal(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return total;
    }

    public void CleanAfterCheckoutDB(string username) {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "DELETE FROM ShoppingCart WHERE username = @USERNAME";
        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        addNew.Parameters.AddWithValue("@USERNAME", username);

        addNew.ExecuteNonQuery();  
        connection.Close();
    }

    public int GetCardQuantityForStore(int storeID, int cardID) {
        int result = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string getQuantity = "SELECT quantity FROM Inventory WHERE storeID = @STOREID AND cardID = @CARDID";
                    adapter.InsertCommand = new SqlCommand(getQuantity, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@STOREID", SqlDbType.VarChar).Value = storeID;
                    adapter.InsertCommand.Parameters.Add("@CARDID", SqlDbType.VarChar).Value = cardID;

                    adapter.InsertCommand.Transaction = transaction;
                    result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return result;
    }

    public void AdjustStockDB(string username, List<PokemonCard> cards) {
        int cardQuantityFromStore;
        foreach(PokemonCard card in cards) {
            cardQuantityFromStore = GetCardQuantityForStore(card.StoreID, card.CardID);
            if (cardQuantityFromStore == card.Quantity) {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                string sqlCmd = "DELETE FROM Inventory WHERE cardID = @CARDID";
                string sqlCmd2 = "DELETE FROM PokemonCard WHERE cardID = @CARDID";
                using SqlCommand addNew2 = new SqlCommand(sqlCmd2, connection);
                using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
                addNew2.Parameters.AddWithValue("@CARDID", card.CardID);
                addNew.Parameters.AddWithValue("@CARDID", card.CardID);

                addNew2.ExecuteNonQuery();
                addNew.ExecuteNonQuery();  
                connection.Close();
            } else if (cardQuantityFromStore > card.Quantity) {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                string sqlCmd = "UPDATE Inventory SET quantity = @QUANTITY WHERE cardID = @CARDID";
                using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
                addNew.Parameters.AddWithValue("@QUANTITY", cardQuantityFromStore - card.Quantity);
                addNew.Parameters.AddWithValue("@CARDID", card.CardID);

                addNew.ExecuteNonQuery();  
                connection.Close();
            } else Console.WriteLine("Something messed up and you overbought.");
        }
    }

    public bool OrderExists() {
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string checkForOrder = "SELECT COUNT (*) orderNumber FROM OrderSummary";
                    adapter.InsertCommand = new SqlCommand(checkForOrder, connection, transaction);

                    adapter.InsertCommand.Transaction = transaction;
                    int result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                    if (result != 0)
                        return true;
                    else return false;
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return false;
    }

    public int GetMaxOrderNumber() {
        int maxNum = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string max = "SELECT MAX(orderNumber) FROM OrderSummary";
                    adapter.InsertCommand = new SqlCommand(max, connection, transaction);

                    adapter.InsertCommand.Transaction = transaction;
                    
                    maxNum = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return maxNum;
    }

    public void CheckoutDB(string username, List<PokemonCard> cards, decimal price) {
        decimal total = GetTotalPriceDB(username);
        int orderNumber;
        if (OrderExists())
            orderNumber = GetMaxOrderNumber();
        else orderNumber = 0;

        orderNumber++;
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "INSERT INTO OrderSummary (orderNumber, username, storeID, cardID, quantity, orderDate, totalPrice) VALUES (@ORDERNUMBER, @USERNAME, @STOREID, @CARDID, @QUANTITY, @ORDERDATE, @TOTALPRICE)";
        /*
            string sqlCmd = "INSERT INTO OrderSummary (username, storeID, cardID, quantity, orderDate, totalPrice) 
            VALUES (@USERNAME, @STOREID, @CARDID, @QUANTITY, @ORDERDATE, @TOTALPRICE)";
        */
        foreach(PokemonCard card in cards) {
            using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
            addNew.Parameters.AddWithValue("@ORDERNUMBER", orderNumber);
            addNew.Parameters.AddWithValue("@USERNAME", username);
            addNew.Parameters.AddWithValue("@STOREID", card.StoreID);
            addNew.Parameters.AddWithValue("@CARDID", card.CardID);
            addNew.Parameters.AddWithValue("@QUANTITY", card.Quantity);
            addNew.Parameters.AddWithValue("@ORDERDATE", DateTime.Today);
            addNew.Parameters.AddWithValue("@TOTALPRICE", total);
            addNew.ExecuteNonQuery();
        }
        connection.Close();
        CleanAfterCheckoutDB(username);
        AdjustStockDB(username, cards);
    }

    public List<StoreOrder> UserOrderHistoryDB(string username) {
        List<StoreOrder> storeOrders = new List<StoreOrder>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string sqlCmd = "SELECT * FROM OrderSummary WHERE username = @USERNAME";
        
        /*
            SELECT * FROM OrderSummary 
            INNER JOIN StoreFront ON StoreFront.storeID = OrderSummary.storeID
            INNER JOIN Inventory ON Inventory.cardID = OrderSummary.cardID
            INNER JOIN PokemonCard ON PokemonCard.cardID = Inventory.cardID
            INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID
            INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID
            WHERE username = @USERNAME";
        */
        
        DataSet Set = new DataSet();

        using SqlDataAdapter Adapter = new SqlDataAdapter(sqlCmd, connection);
        Adapter.SelectCommand.Parameters.AddWithValue("@USERNAME", username);

        Adapter.Fill(Set, "OrderSummary");

        DataTable? Table = Set.Tables["OrderSummary"];

        if (Table != null) {
            foreach(DataRow row in Table.Rows) {
                StoreOrder order = new StoreOrder();
                order.OrderNumber = Convert.ToInt32(row["orderNumber"]);
                order.StoreID = Convert.ToInt32(row["storeID"]);
                order.CardID = Convert.ToInt32(row["cardID"]);
                order.Quantity = Convert.ToInt32(row["quantity"]);
                order.Date = Convert.ToString(row["orderDate"]);
                order.TotalPrice = Convert.ToDecimal(row["TotalPrice"]);

                storeOrders.Add(order);
            }
        }
        return storeOrders;
    }

    public bool MyStoreExistDB(string s) {
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string checkForUsername = "SELECT COUNT (*) username FROM StoreFront WHERE username = @USERNAME";
                    adapter.InsertCommand = new SqlCommand(checkForUsername, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = s;

                    adapter.InsertCommand.Transaction = transaction;
                    int result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                    if (result != 0)
                        return true;
                    else return false;
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return false;
    }

    public void CreateYourStoreDB(string username, string city, string state) {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "INSERT INTO StoreFront (username, City, State) VALUES (@USERNAME, @CITY, @STATE)";
        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        addNew.Parameters.AddWithValue("@USERNAME", username);
        addNew.Parameters.AddWithValue("@CITY", city);
        addNew.Parameters.AddWithValue("@STATE", state);

        addNew.ExecuteNonQuery();
        connection.Close();
    }

    public List<PokemonCard> MyStoreCardsDB(string s) {
        List<PokemonCard> cards = new List<PokemonCard>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string cardsSelect = "SELECT * FROM PokemonCard INNER JOIN Inventory ON Inventory.CardID = PokemonCard.CardID INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID INNER JOIN StoreFront ON StoreFront.storeID = Inventory.storeID WHERE username = @USERNAME ORDER BY cardName";

        DataSet CardSet = new DataSet();

        using SqlDataAdapter cardAdapter = new SqlDataAdapter(cardsSelect, connection);
        cardAdapter.SelectCommand.Parameters.AddWithValue("@USERNAME", s);

        cardAdapter.Fill(CardSet, "PokemonCard");

        DataTable? PokemonCardTable = CardSet.Tables["PokemonCard"];

        if (PokemonCardTable != null) {
            foreach(DataRow row in PokemonCardTable.Rows) {
                PokemonCard card = new PokemonCard();
                card.StoreID = Convert.ToInt32(row["storeID"]);
                card.CardID = Convert.ToInt32(row["cardID"]);
                card.CardName = Convert.ToString(row["cardName"]);
                card.CardSet = Convert.ToString(row["cardSet"]);
                card.ConditionTitle = Convert.ToString(row["conditionTitle"]);
                card.FoilTitle = Convert.ToString(row["foilTitle"]);
                card.Price = Convert.ToDecimal(row["price"]);
                card.Quantity = Convert.ToInt32(row["quantity"]);

                cards.Add(card);
            }
        }
        return cards;
    }

    public void DeleteCardFromMyStoreDB(PokemonCard card, string s) {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "DELETE Inventory FROM Inventory INNER JOIN StoreFront ON StoreFront.storeID = Inventory.storeID INNER JOIN PokemonCard ON PokemonCard.cardID = Inventory.cardID WHERE username = @USERNAME AND Inventory.cardID = @CARDID";
        string sqlCmd2 = "DELETE PokemonCard FROM PokemonCard INNER JOIN Inventory ON Inventory.cardID = PokemonCard.cardID INNER JOIN StoreFront ON StoreFront.storeID = Inventory.storeID WHERE username = @USERNAME AND Inventory.cardID = @CARDID";

        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        using SqlCommand addNew2 = new SqlCommand(sqlCmd2, connection);
        addNew.Parameters.AddWithValue("@USERNAME", s);
        addNew.Parameters.AddWithValue("@CARDID", card.CardID);
        addNew2.Parameters.AddWithValue("@USERNAME", s);
        addNew2.Parameters.AddWithValue("@CARDID", card.CardID);

        addNew2.ExecuteNonQuery();
        addNew.ExecuteNonQuery();
        connection.Close();
    }

    public int GetStoreID(string s) {
        int result = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string sqlCmd = "SELECT storeID FROM StoreFront WHERE username = @USERNAME";
                    adapter.InsertCommand = new SqlCommand(sqlCmd, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = s;

                    adapter.InsertCommand.Transaction = transaction;
                    result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return result;
    }

    public int GetCardID(string s) {
        int result = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString)) {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    string sqlCmd = "SELECT MAX(cardID) FROM Inventory INNER JOIN StoreFront ON StoreFront.storeID = Inventory.storeID WHERE username = @USERNAME";
                    adapter.InsertCommand = new SqlCommand(sqlCmd, connection, transaction);
                    adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = s;

                    adapter.InsertCommand.Transaction = transaction;
                    result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
            }
        }
        return result;
    }
    public void AddCardToStoreDB(string s, string cardName, string cardSet, int conditionID, int foilID, decimal price, int quantity) {
        int storeID = GetStoreID(s);
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "INSERT INTO Inventory (storeID, quantity) VALUES (@STOREID, @QUANTITY)";
        string sqlCmd2 = "INSERT INTO PokemonCard (cardID, cardName, cardSet, conditionID, foilID, price) VALUES (@CARDID, @CARDNAME, @CARDSET, @CONDITIONID, @FOILID, @PRICE)";

        using SqlCommand addNew = new SqlCommand(sqlCmd, connection);
        using SqlCommand addNew2 = new SqlCommand(sqlCmd2, connection);
        addNew.Parameters.AddWithValue("@STOREID", storeID);
        addNew.Parameters.AddWithValue("@QUANTITY", quantity);

        int cardID = GetCardID(s);
        addNew.ExecuteNonQuery();
        addNew2.Parameters.AddWithValue("@CARDID", cardID);
        addNew2.Parameters.AddWithValue("@CARDNAME", cardName);
        addNew2.Parameters.AddWithValue("@CARDSET", cardSet);
        addNew2.Parameters.AddWithValue("@CONDITIONID", conditionID);
        addNew2.Parameters.AddWithValue("@FOILID", foilID);
        addNew2.Parameters.AddWithValue("@PRICE", price);

        addNew2.ExecuteNonQuery();
        
        connection.Close();
    }
}