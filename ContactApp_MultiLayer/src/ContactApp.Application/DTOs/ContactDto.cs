namespace ContactApp.Application.DTOs
{
    public record ContactDto(int Id, string FullName, string Email, string? Phone, DateTime? DateOfBirth, string? Address);
}
