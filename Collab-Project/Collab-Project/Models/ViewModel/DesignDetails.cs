using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Collab_Project.Models.ViewModels
{
    public class DesignDetails
    {
        public DesignDto SelectedDesign { get; set; }
        public IEnumerable<TrailDto> RelatedTrails { get; set; }
    }


}