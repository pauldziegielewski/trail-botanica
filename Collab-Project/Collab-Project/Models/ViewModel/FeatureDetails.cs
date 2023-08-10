using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collab_Project.Models.ViewModel
{
    public class FeatureDetails
    {

        public FeatureDto SelectedFeature { get; set; }
        public IEnumerable<TrailDto> TrailFeatures { get; set; }
        public bool IsAdmin { get; set; }

    }
}