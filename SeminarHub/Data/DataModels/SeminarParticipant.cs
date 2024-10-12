using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.DataModels
{
    public class SeminarParticipant
    {
        [Required]
        public int SeminarId { get; set; }

        [ForeignKey(nameof(SeminarId))]
        public Seminar Seminar { get; set; } = null!;

        [Required]
        public string ParticipantId { get; set; } = string.Empty;

        public IdentityUser Participant { get; set; } = null!;
    }
}

//•	Has SeminarId – integer, PrimaryKey, foreign key (required)
//•	Has Seminar – Seminar
//•	Has ParticipantId – string, PrimaryKey, foreign key (required)
//•	Has Participant – IdentityUser
