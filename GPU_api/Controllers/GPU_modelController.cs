#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using GPU_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GPU_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPU_modelController : ControllerBase
    {
        private readonly GPUContext _context;

        public GPU_modelController(GPUContext context)
        {
            _context = context;
        }

        // GET: api/GPU_model
        [SwaggerOperation(Summary = "Get all GPU models")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPU_model>>> GetGPU_Models()
        {
            return await _context.GPU_Models.ToListAsync();
        }

        [SwaggerOperation(Summary = "Get stores from input model name")]
        [HttpGet("get_stores_with_model/{model_name}")]
        public async Task<ContentResult> GetStores(string model_name)
        {
            var models = await GetGPU_Models();
            
            JArray stores_with_model = new JArray();
            JObject name_obj = new JObject();
            name_obj["model_name"] = model_name;

            foreach(var model in models.Value)
            {
                if(model.model_name.Contains(model_name))
                {
                    JObject matching_model = new JObject();
                    matching_model["store_name"] = model.store_name;
                    matching_model["location"] = model.location;
                    matching_model["brand"] = model.brand;
                    matching_model["price"] = model.price;
                    matching_model["quantity"] = model.quantity;
                    stores_with_model.Add(matching_model);
                }
            }
            name_obj["stores"] = stores_with_model;
            return Content(name_obj.ToString(), "application/json"); 
        }

        [SwaggerOperation(Summary = "Get models from a certain store")]
        [HttpGet("get_models_from_store/{store_name}")]
        public async Task<ContentResult> GetModelsFromStore(string store_name)
        {
            var models = await GetGPU_Models();

            JArray models_from_store = new JArray();
            JObject name_obj = new JObject();
            name_obj["store_name"] = store_name;

            foreach(var model in models.Value)
            {
                if(model.store_name.Contains(store_name))
                {
                    JObject matching_model = new JObject();
                    matching_model["model_name"] = model.model_name;
                    matching_model["brand"] = model.brand;
                    matching_model["location"] = model.location;
                    matching_model["price"] = model.price;
                    matching_model["quantity"] = model.quantity;
                    models_from_store.Add(matching_model);
                }
            }
            name_obj["models"] = models_from_store;
            return Content(name_obj.ToString(), "application/json");
        }

        // GET: api/GPU_model/5
        [SwaggerOperation(Summary = "Get a specific GPU model")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GPU_model>> GetGPU_model(int id)
        {
            var gPU_model = await _context.GPU_Models.FindAsync(id);

            if (gPU_model == null)
            {
                return NotFound();
            }

            return gPU_model;
        }

        // PUT: api/GPU_model/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [SwaggerOperation(Summary = "Update a specific GPU model")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGPU_model(int id, GPU_model gPU_model)
        {
            if (id != gPU_model.sku)
            {
                return BadRequest();
            }

            _context.Entry(gPU_model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GPU_modelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GPU_model
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [SwaggerOperation(Summary = "Create a GPU model")]
        [HttpPost]
        public async Task<ActionResult<GPU_model>> PostGPU_model(GPU_model gPU_model)
        {
            _context.GPU_Models.Add(gPU_model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGPU_model", new { id = gPU_model.sku }, gPU_model);
        }

        // DELETE: api/GPU_model/5
        [SwaggerOperation(Summary = "Delete a specific GPU model")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGPU_model(int id)
        {
            var gPU_model = await _context.GPU_Models.FindAsync(id);
            if (gPU_model == null)
            {
                return NotFound();
            }

            _context.GPU_Models.Remove(gPU_model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool GPU_modelExists(int id)
        {
            return _context.GPU_Models.Any(e => e.sku == id);
        }
    }
}
