using Microsoft.EntityFrameworkCore;
using System;

namespace ZH.App
{
    public partial class MovieDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public MovieDbContext([System.Diagnostics.CodeAnalysis.NotNull] DbContextOptions options)
            : base(options)
        {
        }

        public MovieDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder // .UseLazy...
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\Movies.mdf;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasOne(actor => actor.Movie)
                    .WithMany(movie => movie.Actors);
                    //.HasForeignKey(actor => actor.MovieId)   // already marked Actor.MovieId with [ForeignKey]
                    //.OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                //entity.HasIndex(movie => movie.Id);      // already marked Movie.Id with [Key]
                /*
                  entity.Property(movie => movie.Name)
                    .IsRequired()       // [Required]
                    .HasMaxLength(50);  // [MaxLength(50)]
                */
            });
        }
    }
}
