using Models;
namespace DL;
public interface IRepo
{
    List<PokemonCard> GetAllPokemonCards();
    bool CheckDbForUsername(string s);
}
