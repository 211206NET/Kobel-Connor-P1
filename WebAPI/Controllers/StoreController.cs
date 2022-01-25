using Microsoft.AspNetCore.Mvc;
using Models;
using BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IBL _bl;

        public StoreController(IBL bl)
        {
            _bl = bl;
        }

        [HttpGet("getAllPokemonCards")]
        public List<PokemonCard> GetCards()
        {
            return _bl.GetAllPokemonCards();
        }

        [HttpGet("getAllStoreFronts")]
        public List<StoreFront> GetStores()
        {
            return _bl.GetAllStoreFronts();
        }

        [HttpGet("getStoreCards/{storeID}")]
        public List<PokemonCard> Get(int storeID)
        {
            return _bl.GetStoreCards(storeID);
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