using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants.DataConstants;

namespace SeminarHub.Data.DataModels
{
    [Comment("Category of each seminar")]
    public class Category
    {
        [Key]
        [Comment("Seminar identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Seminar name")]
        public string Name { get; set; } = string.Empty;

        public IList<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}

//•	Has Id – a unique integer, Primary Key
//•	Has Name – string with min length 3 and max length 50 (required)
//•	Has Seminars – a collection of type Seminar
