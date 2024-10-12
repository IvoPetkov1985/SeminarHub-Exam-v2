using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants.DataConstants;

namespace SeminarHub.Models
{
    public class AddSeminarViewModel
    {
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
        [RegularExpression(SeminarDateAndTimeRegex, ErrorMessage = SeminarDateAndTimeErrorMsg)]
        public string DateAndTime { get; set; } = string.Empty;

        [Required(ErrorMessage = SeminarDurationErrorMsg)]
        [Range(SeminarDurationMin, SeminarDurationMax)]
        public int Duration { get; set; }

        public string? OrganizerId { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
