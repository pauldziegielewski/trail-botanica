﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Collab_Project.Models.ViewModels
{
    public class LocationDetails
    {
        public LocationDto SelectedLocation { get; set; }
        public IEnumerable<TrailDto> RelatedTrails { get; set; }

        public bool IsAdmin { get; set; }
    }


}