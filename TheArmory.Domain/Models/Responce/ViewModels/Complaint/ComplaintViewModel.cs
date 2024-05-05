using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Responce.ViewModels.Complaint;

public class ComplaintViewModel
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    
    public ComplaintViewModel(){}

    public ComplaintViewModel(Database.Complaint complaint)
    {
        Description = complaint.Description;
        UserName = complaint.User.Name;
    }
}