using Microsoft.AspNetCore.Mvc.Rendering;

namespace JuliePro.Models
{
    public class RecordViewModel
    {
        public Record Record { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Statuses { get; set; }

    }
}
