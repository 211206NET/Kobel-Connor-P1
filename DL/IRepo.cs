using Models;
namespace DL;
public interface IRepo
{
    List<PokemonCard> GetAllPokemonCards();
    bool CheckDbForUsername(string s);
    bool LoginDB(string u, string p);
    void AddNewUserDB(Customer c);
}
