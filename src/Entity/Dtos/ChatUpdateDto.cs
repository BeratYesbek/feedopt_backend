using System;
using Core.Entity.Abstracts;
using Entity.Concretes;

namespace Entity.Dtos;

public class ChatUpdateDto : IDto
{

    public Chat Chat { get; set; }
    public string ReceiverEmail { get; set; }
    public string SenderEmail { get; set; }
    
}