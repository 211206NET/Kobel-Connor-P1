using Models;

namespace BL;
public interface IBL
{
    List<PokemonCard> GetAllPokemonCards();
    bool CheckUsernameExists(string s);
    bool Login(string u, string p);
    void AddNewUser(Customer c);
    List<StoreFront> GetAllStoreFronts();
    bool CardAlreadyInShoppingCart(PokemonCard card);
    void AddCardToShoppingCart(PokemonCard card, int quantity, string username);
    int CardsAvailableForIndividualStore(StoreFront store);
    List<PokemonCard> GetStoreCards(int storeID);
    List<PokemonCard> ShowYourCart(string s);
    void DeleteCardFromShoppingCart(PokemonCard card, string s);
    void Checkout(string s, List<PokemonCard> cards, decimal price);
    List<StoreOrder> UserOrderHistory(string s);
    bool MyStoreExist(string s);
    void CreateYourStore(string username, string city, string state);
    List<PokemonCard> MyStoreCards(string s);
    void DeleteCardFromMyStore(PokemonCard card, string s);
    void AddCardToStore(string s, string cardName, string cardSet, int conditionID, int foilID, decimal price, int quantity);
}