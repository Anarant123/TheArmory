using TheArmory.Domain.Models.Database;

namespace TheArmory.Domain.Models.Responce.ViewModels.Ad;

public class TileAdViewModel
{
    public Guid Id { get; set; }
    
    public List<Media> Images { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime CreationDateTime { get; set; }


    public TileAdViewModel(Database.Ad ad)
    {
        Id = ad.Id;
        Images = ad.Medias;
        Price = ad.Price;
        CreationDateTime = ad.CreationDateTime;
    }
}