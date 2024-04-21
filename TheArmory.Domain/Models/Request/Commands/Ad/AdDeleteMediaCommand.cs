using System;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdDeleteMediaCommand : AdCommand
{
    public Guid MediaId { get; set; }
}