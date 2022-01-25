using Microsoft.AspNetCore.Mvc;
using Models;
using BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBL _bl;

        public UserController(IBL bl)
        {
            _bl = bl;
        }

        [HttpGet("login/{username}/{password}")]
        public bool Get(string username, string password)
        {
            return _bl.Login(username, password);
        }

        [HttpPost("addNewUser/{username}/{password}/{firstname}/{lastname}/{email}")]
        public void Post(string username, string password, string firstname, string lastname, string email)
        {
            if (_bl.CheckUsernameExists(username))
            {
                Console.WriteLine("Username already exists");
            }
            else
            {
                Customer customer = new Customer();
                customer.FirstName = firstname;
                customer.LastName = lastname;
                customer.Email = email;
                customer.Username = username;
                customer.Password = password;
                _bl.AddNewUser(customer);
            }
        }

        [HttpPut("addCardToShoppingCart/{cardNumber}/{quantity}/{username}")]
        public void Put(int cardNumber, int quantity, string username)
        {
            List<PokemonCard> cards = _bl.GetAllPokemonCards();
            _bl.AddCardToShoppingCart(cards[cardNumber - 1], quantity, username);
        }


        [HttpGet("showYourCart/{username}")]
        public List<PokemonCard> GetCart(string username)
        {
            return _bl.ShowYourCart(username);
        }

        [HttpDelete("deleteCardFromShoppingCart/{cardNumber}/{username}")]
        public void Delete(int cardNumber, string username)
        {
            List<PokemonCard> cards = _bl.ShowYourCart(username);
            _bl.DeleteCardFromShoppingCart(cards[cardNumber - 1], username);
        }

        [HttpPost("checkout/{username}")]
        public void Post(string username)
        {
            List<PokemonCard> cards = new List<PokemonCard>();
            decimal runningTotal = 0;
            foreach (PokemonCard card in cards)
            {
                runningTotal += card.Price;
            }

            _bl.Checkout(username, _bl.ShowYourCart(username), runningTotal);
        }

        [HttpGet("userOrderHistory/{username}")]
        public List<StoreOrder> GetOrders(string username)
        {
            return _bl.UserOrderHistory(username);
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