using System;
using System.Collections.Generic;

namespace ChirpAPI.Services.Model.ViewModel;

public class ChirpViewModel
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string? ExtUrl { get; set; }

    public DateTime CreationTime { get; set; }

    public double? Lat { get; set; }

    public double? Lng { get; set; }

   

}
