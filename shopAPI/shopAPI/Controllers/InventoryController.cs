using Assignment1;
using Microsoft.AspNetCore.Mvc;
using shopAPI.EC;


namespace shopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<Item>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpDelete("/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost()]
        public async Task<Item> AddOrUpdate([FromBody] Item p)
        {
            return await new InventoryEC().AddOrUpdate(p);
        }
    }
}
