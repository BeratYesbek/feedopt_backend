using Core.Entity.Abstracts;
using Core.Utilities.Language;

namespace Entity.Dtos;

public class UserDto : IDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public PreferredLanguage PreferredLanguage { get; set; } = PreferredLanguage.tr;
    
    public string ImagePath { get; set; }
    
}