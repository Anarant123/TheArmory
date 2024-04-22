namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdToComplaintCommand : AdCommand
{
    /// <summary>
    /// Описание жалобы
    /// </summary>
    public string Description { get; set; }
}