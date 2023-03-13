using API.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> Users {get; set; }
    // πίνακας με δεδομένα μέσα στη βάση , θα είναι ο πίνακας users ,παίρνει στοιχεία από την AppUser.cs . Δημιουργούμε στο appsettings ένα connection που θ δημιουργήσει τον πίνακα users// 
    }
}

