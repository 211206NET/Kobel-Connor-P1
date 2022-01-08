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

        // int count = 0;
        // bool checkPass;
        // if (CheckDbForUsername(u)) {
        //     count = 1;
        // }

        // using SqlConnection connection = new SqlConnection(_connectionString);
        // connection.Open();
        // string hashPassword = "SELECT hashPassword FROM Customer WHERE username = @USERNAME";
        // using SqlCommand login = new SqlCommand(hashPassword, connection);
        // login.Parameters.AddWithValue("@USERNAME", u);
        // login.ExecuteNonQuery();

        // DataSet NameSet = new DataSet();

        // using SqlDataAdapter nameAdapter = new SqlDataAdapter(hashPassword, connection);

        // nameAdapter.Fill(NameSet, "Customer");

        // DataTable? NameTable = NameSet.Tables["Customer"];
        // connection.Close();

        // foreach (DataRow row in NameTable.Rows) {
        //     Console.WriteLine(row.hashPassword);
        // }

        
        // if (count == 1 && BC.Verify(p, hashPass)){
        //     return true;
        // }
        // else return false;

        // using (SqlConnection connection = new SqlConnection(_connectionString)) {
        //     connection.Open();
        //     using (SqlTransaction transaction = connection.BeginTransaction()) {
        //         try {
        //             string checkForUsername = "SELECT COUNT (*) username FROM Customer WHERE username = @USERNAME";
        //             string hashPass = "SELECT hashPassword FROM Customer WHERE username = @USERNAME";
        //             adapter.InsertCommand = new SqlCommand(checkForUsername, connection, transaction);
        //             adapter.InsertCommand = new SqlCommand(hashPass, connection, transaction);
        //             adapter.InsertCommand.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = u;

        //             adapter.InsertCommand.Transaction = transaction;
        //             int result = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

        //             transaction.Commit();
        //             connection.Close();
        //             if (result != 0 && BC.Verify(p, hashPass)) {
        //                 return true;
        //             }
        //             else return false;
        //         }
        //         catch (SqlException ex) {
        //             Console.WriteLine(ex.Message);
        //             Console.WriteLine(ex.Source);
        //         }
        //     }
        // }
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
        string cardsSelect = "SELECT cardName, cardSet, Condition.conditionTitle, Foil.foilTitle, price FROM PokemonCard INNER JOIN Condition ON Condition.conditionID = PokemonCard.conditionID INNER JOIN Foil ON Foil.foilID = PokemonCard.foilID ORDER BY cardName";

        DataSet CardSet = new DataSet();

        using SqlDataAdapter cardAdapter = new SqlDataAdapter(cardsSelect, connection);

        cardAdapter.Fill(CardSet, "PokemonCard");

        DataTable? PokemonCardTable = CardSet.Tables["PokemonCard"];

        if (PokemonCardTable != null) {
            foreach(DataRow row in PokemonCardTable.Rows) {
                PokemonCard card = new PokemonCard();
                card.CardName = Convert.ToString(row["cardName"]);
                card.CardSet = Convert.ToString(row["cardSet"]);
                card.ConditionTitle = Convert.ToString(row["conditionTitle"]);
                card.FoilTitle = Convert.ToString(row["foilTitle"]);
                card.Price = Convert.ToDecimal(row["price"]);

                allPokemonCards.Add(card);
            }
        }

        return allPokemonCards;
    }
}