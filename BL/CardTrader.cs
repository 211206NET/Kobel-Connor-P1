using Models;
using DL;

namespace BL;
public class CardTrader : IBL {
    private IRepo _dl;

    public CardTrader(IRepo repo) {
        _dl = repo;
    }

    /// <summary>
    /// gets all pokemon cards from all stores
    /// </summary>
    /// <returns>list of pokemon cards</returns>
    public List<PokemonCard> GetAllPokemonCards() {
        List<PokemonCard> pokemonCards = _dl.GetAllPokemonCardsDB();
        return pokemonCards;
    }

    /// <summary>
    /// tells if card alread is in shopping cart
    /// </summary>
    /// <param name="card"></param>
    /// <returns>boolean</returns>
    public bool CardAlreadyInShoppingCart(PokemonCard card) {
        if (_dl.CardAlreadyInShoppingCartDB(card))
            return true;
        else return false;
    }

    /// <summary>
    /// adds card(s) to the shopping cart of username
    /// </summary>
    /// <param name="card"></param>
    /// <param name="quantity"></param>
    /// <param name="username"></param>
    public void AddCardToShoppingCart(PokemonCard card, int quantity, string username) {
        _dl.AddCardToShoppingCartDB(card, quantity, username);
    }

    /// <summary>
    /// checks to see if username exists
    /// </summary>
    /// <param name="s"></param>
    /// <returns>boolean</returns>
    public bool CheckUsernameExists(string s) {
        if (_dl.CheckDbForUsername(s)) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// adds new user
    /// </summary>
    /// <param name="c"></param>
    public void AddNewUser(Customer c) {
        _dl.AddNewUserDB(c);
    }

    /// <summary>
    /// checks to see if username and password is correct
    /// </summary>
    /// <param name="u"></param>
    /// <param name="p"></param>
    /// <returns>boolean</returns>
    public bool Login(string u, string p) {
        if (_dl.LoginDB(u, p)) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// gets a list of StoreFronts
    /// </summary>
    /// <returns>list of StoreFronts</returns>
    public List<StoreFront> GetAllStoreFronts() {
        List<StoreFront> allStores = _dl.GetAllStoreFrontsDB();
        return allStores;
    }

    /// <summary>
    /// gets the number of cards a store is selling
    /// </summary>
    /// <param name="store"></param>
    /// <returns>number of cards in storefront</returns>
    public int CardsAvailableForIndividualStore(StoreFront store) {
        return _dl.CardsAvailableForIndividualStoreDB(store);
    }

    /// <summary>
    /// gets the cards from a specific store
    /// </summary>
    /// <param name="storeID"></param>
    /// <returns>list of pokemon cards</returns>
    public List<PokemonCard> GetStoreCards(int storeID) {
        List<PokemonCard> pokemonCards = _dl.GetStoreCardsDB(storeID);
        return pokemonCards;
    }

    /// <summary>
    /// gets the users shopping cart
    /// </summary>
    /// <param name="s"></param>
    /// <returns>list of cards in shopping cart</returns>
    public List<PokemonCard> ShowYourCart(string s) {
        List<PokemonCard> cards = _dl.ShowYourCartDB(s);
        return cards;
    }

/// <summary>
/// deletes card from shopping cart
/// </summary>
/// <param name="card"></param>
/// <param name="username"></param>
    public void DeleteCardFromShoppingCart(PokemonCard card, string username) {
        _dl.DeleteCardFromShoppingCartDB(card, username);
    }

    /// <summary>
    /// creates order summary, updates inventory, clears shopping cart
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cards"></param>
    /// <param name="price"></param>
    public void Checkout(string username, List<PokemonCard> cards, decimal price) {
        _dl.CheckoutDB(username, cards, price);
    }
    
    /// <summary>
    /// gets user's order history
    /// </summary>
    /// <param name="s"></param>
    /// <returns>list of store orders</returns>
    public List<StoreOrder> UserOrderHistory(string s) {
        List<StoreOrder> storeOrders = _dl.UserOrderHistoryDB(s);
        return storeOrders;
    }

    /// <summary>
    /// tells if user's storefront exists
    /// </summary>
    /// <param name="s"></param>
    /// <returns>boolean</returns>
    public bool MyStoreExist(string s) {
        if (_dl.MyStoreExistDB(s)) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// creates the users storefront
    /// </summary>
    /// <param name="username"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    public void CreateYourStore(string username, string city, string state) {
        _dl.CreateYourStoreDB(username, city, state);
    }

    /// <summary>
    /// gets the cards from user's storefront
    /// </summary>
    /// <param name="s"></param>
    /// <returns>list of cards from user's storefront</returns>
    public List<PokemonCard> MyStoreCards(string s) {
        List<PokemonCard> cards = _dl.MyStoreCardsDB(s);
        return cards;
    }

    /// <summary>
    /// deletes a card from user's storefront
    /// </summary>
    /// <param name="card"></param>
    /// <param name="s"></param>
    public void DeleteCardFromMyStore(PokemonCard card, string s) {
        _dl.DeleteCardFromMyStoreDB(card, s);
    }

    /// <summary>
    /// adds new card to user's storefront
    /// </summary>
    /// <param name="s"></param>
    /// <param name="cardName"></param>
    /// <param name="cardSet"></param>
    /// <param name="conditionID"></param>
    /// <param name="foilID"></param>
    /// <param name="price"></param>
    /// <param name="quantity"></param>
    public void AddCardToStore(string s, string cardName, string cardSet, int conditionID, int foilID, decimal price, int quantity) {
        _dl.AddCardToStoreDB(s, cardName, cardSet, conditionID, foilID, price, quantity);
    }
}