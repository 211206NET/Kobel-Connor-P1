using Models;
using DL;

namespace BL;
public class CardTrader : IBL {
    private IRepo _dl;

    public CardTrader(IRepo repo) {
        _dl = repo;
    }

    public List<PokemonCard> GetAllPokemonCards() {
        List<PokemonCard> pokemonCards = _dl.GetAllPokemonCardsDB();
        return pokemonCards;
    }

    public bool CardAlreadyInShoppingCart(PokemonCard card) {
        if (_dl.CardAlreadyInShoppingCartDB(card))
            return true;
        else return false;
    }

    public void AddCardToShoppingCart(PokemonCard card, int quantity, string username) {
        _dl.AddCardToShoppingCartDB(card, quantity, username);
    }

    public bool CheckUsernameExists(string s) {
        if (_dl.CheckDbForUsername(s)) {
            return true;
        }
        return false;
    }

    public void AddNewUser(Customer c) {
        _dl.AddNewUserDB(c);
    }

    public bool Login(string u, string p) {
        if (_dl.LoginDB(u, p)) {
            return true;
        }
        return false;
    }

    public List<StoreFront> GetAllStoreFronts() {
        List<StoreFront> allStores = _dl.GetAllStoreFrontsDB();
        return allStores;
    }

    public int CardsAvailableForIndividualStore(StoreFront store) {
        return _dl.CardsAvailableForIndividualStoreDB(store);
    }

    public List<PokemonCard> GetStoreCards(int storeID) {
        List<PokemonCard> pokemonCards = _dl.GetStoreCardsDB(storeID);
        return pokemonCards;
    }

    public List<PokemonCard> ShowYourCart(string s) {
        List<PokemonCard> cards = _dl.ShowYourCartDB(s);
        return cards;
    }

    public void DeleteCardFromShoppingCart(PokemonCard card, string username) {
        _dl.DeleteCardFromShoppingCartDB(card, username);
    }

    public void Checkout(string username, List<PokemonCard> cards, decimal price) {
        _dl.CheckoutDB(username, cards, price);
    }
}