using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants.DataConstants;

namespace SeminarHub.Models
{
    public class JoinSeminarViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(SeminarTopicMaximumLength, MinimumLength = SeminarTopicMinimumLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [StringLength(SeminarLecturerMaximumLength, MinimumLength = SeminarLecturerMinimumLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [StringLength(SeminarDetailsMaximumLength, MinimumLength = SeminarDetailsMinimumLength)]
        public string Details { get; set; } = string.Empty;

        [Required]
        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Range(SeminarDurationMin, SeminarDurationMax)]
        public int Duration { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
