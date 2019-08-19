using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntivirusTesting.Models.FileUploadSample
{
    public class MultipleFileModel
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}