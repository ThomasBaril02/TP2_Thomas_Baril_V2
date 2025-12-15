using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace JuliePro.Models
{
    public class Record
    {
        [Display(Name = "Id")] 
        public int Id { get; set; }
        
        [Display(Name = "Date")] 
        public DateTime Date { get; set; }

        [ForeignKey("Discipline")]
        [Display(Name = "Discipline_Id")] 
        public int? Discipline_Id { get; set; }

        [Display(Name = "Discipline")]
        [ValidateNever]
        public virtual Discipline Discipline { get; set; }
         
        [Display(Name = "Amount")]
        [ValidateNever]
        public Decimal Amount { get; set; }

        [Display(Name = "Unit")]
        [StringLength(50, MinimumLength = 1)]
        public string Unit { get; set; }

        [ForeignKey("Trainer")]
        [Display(Name = "Trainer_Id")] 
        public int? Trainer_Id { get; set; }
        [ValidateNever]
        [Display(Name = "Trainer")] 
        public virtual Trainer Trainer { get; set; }
    }
}