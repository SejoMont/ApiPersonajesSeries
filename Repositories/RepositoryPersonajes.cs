using ApiPersonajesSeries.Data;
using ApiPersonajesSeries.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiPersonajesSeries.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        public async Task<Personaje>
            FindPersonajeAsync(int idPersonaje)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(z => z.IdPersonaje == idPersonaje);
        }

        public async Task InsertPersonajeAsync(Personaje personaje)
        {

            this.context.Personajes.Add(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<string>> GetSeries()
        {
            List<string> series = await this.context.Personajes.Select(s => s.Serie).Distinct().ToListAsync();

            return series;
        }

        public async Task<List<Personaje>> GetPersonajesSeriesAsync(string serie)
        {
            List<Personaje> personajes = await this.context.Personajes.Where(x => x.Serie == serie).ToListAsync();

            return personajes;
        }

        public async Task UpdatePersonajeAsync(Personaje personaje)
        {
            Personaje updatePersonaje = await this.FindPersonajeAsync(personaje.IdPersonaje);

            updatePersonaje.Nombre = personaje.Nombre;
            updatePersonaje.Imagen = personaje.Imagen;
            updatePersonaje.Serie = personaje.Serie;

            await this.context.SaveChangesAsync();
        }

        public async Task DeletePersonajeAsync(int id)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            this.context.Personajes.Remove(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> GetUltimoId()
        {
            var ultimoId = await this.context.Personajes
                                            .MaxAsync(p => (int?)p.IdPersonaje);

            return ultimoId ?? 1;
        }
    }
}
