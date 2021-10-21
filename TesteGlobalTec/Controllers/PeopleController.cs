using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteGlobalTec.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using TesteGlobalTec.Data;

namespace TesteGlobalTec.Controllers
{
    [ApiController]
    [Route("v1/people")]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<People>>> Get([FromServices] DataContext context)
        {
            var peoples = await context.Peoples.ToListAsync();

            return peoples;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<People>> GetById([FromServices] DataContext context, int id)
        {
            var people = await context.Peoples
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            return people;
        }

        [HttpGet]
        [Route("{uf}")]
        [Authorize]
        public async Task<ActionResult<List<People>>> GetByUf([FromServices] DataContext context, string uf)
        {
            var peoples = await context.Peoples.Where(x => x.Uf == uf).ToListAsync();

            return peoples;
        }

        [HttpPost]
        [Route("")]
        [Authorize (Roles = "manager")]
        public async Task<ActionResult<People>> Post(
            [FromServices] DataContext context,
            [FromBody] People model)
        {
            if(ModelState.IsValid)
            {
                context.Peoples.Add(model);
                await context.SaveChangesAsync();

                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut]
        [Route("{id:int}")]
        [Authorize (Roles = "manager")]
        public async Task<ActionResult<People>> Put(
            [FromServices] DataContext context,
            [FromBody] People model,
            int id)
        {
            if(ModelState.IsValid && id == model.Id)
            {
                context.Peoples.Update(model);
                await context.SaveChangesAsync();

                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize (Roles = "manager")]
        public async Task<ActionResult<People>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var people = await context.Peoples.FirstOrDefaultAsync(x => x.Id == id);
            if(people == null)
            {
                return NotFound();
            }

            context.Peoples.Remove(people);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}