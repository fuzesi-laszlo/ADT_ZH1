using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ZH.App
{
    [Table("Movies")]
    public class Movie
    {
        public Movie()
        {
            this.Actors = new HashSet<Actor>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [StringRange("Action", "Comedy", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Thriller")]
        public string Genre { get; set; }

        [StringRange("G", "PG", "PG-13", "R", "NC-17")]
        public string Rating { get; set; }

        public int YearOfRelease { get; set; }

        [NotMapped]
        public virtual ICollection<Actor> Actors { get; set; }

        public override string ToString() => this.MyToString();
    }
}
