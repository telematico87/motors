using eCommerce.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    public class eCommerceContext : IdentityDbContext<eCommerceUser>
    {
        public eCommerceContext() : base("name=eCommerceConnectionString_OK")
        {
            Database.SetInitializer<eCommerceContext>(new eCommerceDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!
            
            modelBuilder.Entity<Promo>()
                .HasIndex(p => new { p.Code })
                .IsUnique(true);

            modelBuilder.Entity<eCommerceUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageResource> LanguageResources { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRecord> CategoryRecords { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRecord> ProductRecords { get; set; }

        public DbSet<Promo> Promos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }

        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductsColors { get; set; }

        //Nuevos atributos 
        public DbSet<Talla> Tallas { get; set; }
        public DbSet<ProductTalla> ProductsTallas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<ProductModelo> ProductsModelos { get; set; }

        public DbSet<Medida> Medidas { get; set; }
        public DbSet<ProductMedida> ProductsMedidas { get; set; }

        public DbSet<Litros> Litroes { get; set; }
        public DbSet<ProductLitros> ProductsLitroes { get; set; }
        public DbSet<Aro> Aros { get; set; }
        public DbSet<ProductsAro> ProductAros { get; set; }

        public DbSet<Financiamiento> Financiamientos { get; set; }

        public DbSet<Catalogo> Catalogos { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Ubigeo> Ubigeos { get; set; }
        public DbSet<TablaMaster> TablaMasters { get; set; }
        public DbSet<TipoCambio> TipoCambios { get; set; }

        public DbSet<Contacto> Contactos { get; set; }


        public static eCommerceContext Create()
        {
            return new eCommerceContext();
        }
    }
}
