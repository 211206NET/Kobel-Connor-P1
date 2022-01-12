using Models;
namespace DL;
public interface IRepo
{
    List<PokemonCard> GetAllPokemonCardsDB();
    bool CheckDbForUsername(string s);
    bool LoginDB(string u, string p);
    void AddNewUserDB(Customer c);
    void AddCardToShoppingCartDB(PokemonCard card, int quantity, string username);
    bool CardAlreadyInShoppingCartDB(PokemonCard card);
    List<StoreFront> GetAllStoreFrontsDB();
    int CardsAvailableForIndividualStoreDB(StoreFront store);
    List<PokemonCard> GetStoreCardsDB(int storeID);
    List<PokemonCard> ShowYourCartDB(string s);
    void DeleteCardFromShoppingCartDB(PokemonCard card, string s);
    void CheckoutDB(string username, List<PokemonCard> cards, decimal price);
    decimal GetTotalPriceDB(string username);
    int GetCardQuantityForStore(int storeID, int cardID);
    void CleanAfterCheckoutDB(string username);
    void AdjustStockDB(string username, List<PokemonCard> cards);
    int GetMaxOrderNumber();
    List<StoreOrder> UserOrderHistoryDB(string s);
    bool MyStoreExistDB(string s);
    void CreateYourStoreDB(string username, string city, string state);
    List<PokemonCard> MyStoreCardsDB(string s);
    void DeleteCardFromMyStoreDB(PokemonCard card, string s);
    void AddCardToStoreDB(string s, string cardName, string cardSet, int conditionID, int foilID, decimal price, int quantity);
}
