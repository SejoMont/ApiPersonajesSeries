using ApiPersonajesSeries.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiPersonajesSeries.Data
{
    public class PersonajesContext : DbContext
    {
        public PersonajesContext(DbContextOptions<PersonajesContext>
            options) : base(options) { }
        public DbSet<Personaje> Personajes { get; set; }
    }
}