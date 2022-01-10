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

    public List<PokemonCard> GetAllPokemonCards() {
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

    public bool CardAlreadyInShoppingCart(PokemonCard card) {
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

    public void AddCardToShoppingCart(PokemonCard card, int quantity, string username) {
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
}