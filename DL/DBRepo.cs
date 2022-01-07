using Microsoft.Data.SqlClient;
using System.Data;
using Models;

namespace DL;

public class DBRepo : IRepo {
    private string _connectionString;
    public DBRepo(string connectionString) {
        _connectionString = connectionString;
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