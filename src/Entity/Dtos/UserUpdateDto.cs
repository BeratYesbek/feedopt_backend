using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Microsoft.AspNetCore.Http;

namespace Entity.Dtos
{
    public class UserUpdateDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public IFormFile File { get; set; }
        public PreferredLanguage PreferredLanguage { get; set; } = PreferredLanguage.tr;
    }
}
