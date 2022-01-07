using Microsoft.Data.SqlClient;
using System.Data;
using Models;
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