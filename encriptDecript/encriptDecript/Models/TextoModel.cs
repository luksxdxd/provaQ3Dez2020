using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace encriptDecript.Models
{
    public class TextoModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Texto:")]
        [DataType(DataType.Text)]
        public String Textoss { get; set; }

        public String CopyTextoss { get; set; }
    }
}