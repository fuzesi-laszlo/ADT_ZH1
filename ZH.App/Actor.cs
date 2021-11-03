using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZH.App
{
    [Table("Actors")]
    public class Actor
    {
        public Actor()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]

        public string Name { get; set; }

        public string Sex { get; set; }

        [ForeignKey(nameof(Actor.Movie))]
        public int MovieId { get; set; }

        [NotMapped]
        public virtual Movie Movie { get; set; }

        public override string ToString() => this.MyToString();
    }
}
