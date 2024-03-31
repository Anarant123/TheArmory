namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdToComplaintCommand
{
    public Guid AdId { get; set; }
    
    /// <summary>
    /// Описание жалобы
    /// </summary>
    public string Description { get; set; }
}