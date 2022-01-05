﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Entity.Concretes;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class ChatValidator : AbstractValidator<Chat>
    {
        public ChatValidator()
        {
            RuleFor(c => c.Message).NotNull().NotEmpty()
                .WithMessage(ChatValidationMessages.CharMessageEmptyMessage);

            RuleFor(c => c.ReceiverId).NotNull().NotEmpty()
                .WithMessage(ChatValidationMessages.ChatReceiverIdEmptyMessage);

            RuleFor(c => c.SenderId).NotNull().NotEmpty()
                .WithMessage(ChatValidationMessages.ChatSenderIdEmptyMessage);

        }
    }
}
