using KeyVaultLib;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyAddressBookPlus;
using System.IO;

namespace AddressWebApp.Models
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = GetSecret.DefaultConnectionString().Result;
            //var connectionString = KeyVaultService.GetSecret().Result;            
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Address> Addresses { get; set; }

    }
}