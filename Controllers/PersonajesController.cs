using ApiPersonajesSeries.Models;
using ApiPersonajesSeries.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesSeries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Personaje>>>
            GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Personaje>>
            FindPersonaje(int id)
        {
            return await this.repo.FindPersonajeAsync(id);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<string>>>
            GetSeries()
        {
            return await this.repo.GetSeries();
        }

        [HttpGet("[action]/{serie}")]
        public async Task<ActionResult<List<Personaje>>>
            GetPersonajesSerie(string serie)
        {
            return await this.repo.GetPersonajesSeriesAsync(serie);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> PostPersonaje
            (Personaje personaje)
        {
            int lastId = await repo.GetUltimoId() + 1;

            personaje.IdPersonaje = lastId;

            await this.repo.InsertPersonajeAsync(personaje);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> PutPersonaje
            (Personaje personaje)
        {
            await this.repo.UpdatePersonajeAsync(personaje);
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeletePersonaje(int id)
        {
            //PODEMOS PERSONALIZAR LA RESPUESTA
            if (await this.repo.FindPersonajeAsync(id) == null)
            {
                //NO EXISTE EL DEPARTAMENTO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeletePersonajeAsync(id);
                return Ok();
            }
        }
    }
}
