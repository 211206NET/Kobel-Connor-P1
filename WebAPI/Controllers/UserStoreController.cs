using Microsoft.AspNetCore.Mvc;
using Models;
using BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoreController : ControllerBase
    {
        private IBL _bl;

        public UserStoreController(IBL bl)
        {
            _bl = bl;
        }

        [HttpPost("createYourStore/{username}/{city}/{state}")]
        public void Post(string username, string city, string state)
        {
            if (_bl.MyStoreExist(username))
            {
                Console.WriteLine("Your store already exists");
            } else
            {
                _bl.CreateYourStore(username, city, state);
            }
        }

        [HttpPut("addCardToStore/{username}/{cardname}/{cardset}/{conditionID}/{foilID}/{price}/{quantity}")]
        public void Put(string username, string cardname, string cardset, int conditionID, int foilID, decimal price, int quantity)
        {
            _bl.AddCardToStore(username, cardname, cardset, conditionID, foilID, price, quantity);
        }

        [HttpGet("myStoreCards/{username}")]
        public List<PokemonCard> Post(string username)
        {
            List<PokemonCard> cards = _bl.MyStoreCards(username);
            return cards;

        }

        [HttpDelete("deleteCardFromMyStore/{cardNumber}/{username}")]
        public void Delete(int cardNumber, string username)
        {
            List<PokemonCard> cards = _bl.MyStoreCards(username);
            _bl.DeleteCardFromMyStore(cards[cardNumber - 1], username);
        }


    }
}


//// GET api/<CardTraderController>/5
//[HttpGet("{id}")]
//public string Get(int id)
//{
//    return "value";
//}

//// POST api/<CardTraderController>d
//[HttpPost]
//public void Post([FromBody] string value)
//{
//}

//// PUT api/<CardTraderController>/5
//[HttpPut("{id}")]
//public void Put(int id, [FromBody] string value)
//{
//}

//// DELETE api/<CardTraderController>/5
//[HttpDelete("{id}")]
//public void Delete(int id)
//{
//}