using Microsoft.AspNet.Identity.EntityFramework;
using ST.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Models.IdentityModels
{
     public class ApplicationRole :IdentityRole
    {
        [StringLength(200)]
        public string Description { get; set; }
        public virtual List<Firma> Firma { get; set; } = new List<Entities.Firma>();
    }
}
