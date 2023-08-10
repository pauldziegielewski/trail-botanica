using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collab_Project.Models
{
    public class Design
    {
        [Key]
        public int Design_Id { get; set; }
        public string Architect_Name { get; set; }
        public string Description { get; set; }
        public string Drawing_Plan { get; set; }

        //A Design can belong to one Trial
        //A Trail can have many designs
        [ForeignKey("Trail")]
        public int TrailID { get; set; }
        public virtual Trail Trail { get; set; }
    }

    public class DesignDto
    {
        public int Design_Id { get; set; }
        public string Architect_Name { get; set; }
        public string Description { get; set; }
        public string Drawing_Plan { get; set; }
        public string TrailName { get; set; }
    }
}