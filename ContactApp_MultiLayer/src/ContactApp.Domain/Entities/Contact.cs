using System.ComponentModel.DataAnnotations;

namespace ContactApp.Domain.Entities
{
    /// <summary>
    /// Aggregate root: Contact entity.
    /// Keep domain model clean of infrastructure concerns.
    /// </summary>
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }
    }
}
