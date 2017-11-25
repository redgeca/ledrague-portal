using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LeDragueCoreObjects.Contracts
{
    public class Template
    {
        public Template()
        {
            this.Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        [Display(Name = "Nom du modèle")]
        public string TemplateName { get; set; }
        public byte[] TemplateContent { get; set; }

        [NotMapped]
        [Display(Name = "Fichier modèle")]
        public IFormFile InnerContent { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
