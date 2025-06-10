using System.ComponentModel.DataAnnotations;

namespace ChirpAPI.Services.Model
{
    public class ChirpUpdateModel
    {
     
        public string Text { get; set; } = null!;

       
        public string? ExtUrl { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}