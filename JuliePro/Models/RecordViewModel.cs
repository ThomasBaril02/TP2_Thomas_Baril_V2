using Microsoft.AspNetCore.Mvc.Rendering;

namespace JuliePro.Models
{
    public class RecordViewModel
    {
        public Record Record { get; set; }
        public SelectList Trainers { get; set; }
        public SelectList Disciplines { get; set; }

    }
}
