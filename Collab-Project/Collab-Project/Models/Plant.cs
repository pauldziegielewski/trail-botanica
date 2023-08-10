using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collab_Project.Models
{
    public class Plant
    {
        [Key]
        public int Plant_Id { get; set; }
        public string Plant_Name { get; set; }
        public string Plant_Type { get; set; }
        public string Plant_Image { get; set; }
        public ICollection<Trail> Trails { get; set; }
    }
    public class PlantDto
    {
        public int Plant_Id { get; set; }
        public string Plant_Name { get; set; }
        public string Plant_Type { get; set; }
        public string Plant_Image { get; set; }
    }
}