using System;
using Core.Entity.Abstracts;
using Core.Utilities.Language;

namespace Entity.Dtos;

public class UserDto : IDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool Status { get; set; }
    public string Roles { get; set; }
    public PreferredLanguage PreferredLanguage { get; set; } = PreferredLanguage.tr;
    public string ImagePath { get; set; }
   /* public DateTime LastLoggedIn { get; set; }
    public DateTime RegisterDate { get; set; }*/
    
}