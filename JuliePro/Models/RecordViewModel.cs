using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JuliePro.Models
{
    public class RecordViewModel
    {
        public Record Record { get; set; }
        [ValidateNever]
        public SelectList Trainers { get; set; }
        [ValidateNever]
        public SelectList Disciplines { get; set; }

    }
}
