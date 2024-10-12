using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstants.DataConstants;

namespace SeminarHub.Data.DataModels
{
    [Comment("The seminar itself")]
    public class Seminar
    {
        [Key]
        [Comment("Seminar identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(SeminarTopicMaximumLength)]
        [Comment("Seminar title / topic")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(SeminarLecturerMaximumLength)]
        [Comment("Seminar lecturer")]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [MaxLength(SeminarDetailsMaximumLength)]
        [Comment("Details about the seminar")]
        public string Details { get; set; } = string.Empty;

        [Required]
        [Comment("Organizer identifier")]
        public string OrganizerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("Date and time of the seminar")]
        public DateTime DateAndTime { get; set; }

        [Range(SeminarDurationMin, SeminarDurationMax)]
        [Comment("Duration in minutes")]
        public int? Duration { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public IList<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}

//•	Has Id – a unique integer, Primary Key
//•	Has Topic – string with min length 3 and max length 100 (required)
//•	Has Lecturer – string with min length 5 and max length 60 (required)
//•	Has Details – string with min length 10 and max length 500 (required)
//•	Has OrganizerId – string (required)
//•	Has Organizer – IdentityUser (required)
//•	Has DateAndTime – DateTime with format "dd/MM/yyyy HH:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has Duration – integer value between 30 and 180
//•	Has CategoryId – integer, foreign key (required)
//•	Has Category – Category (required)
//•	Has SeminarsParticipants – a collection of type SeminarParticipant
