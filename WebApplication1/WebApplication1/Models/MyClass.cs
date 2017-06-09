using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MyClass 
    {
        [Range(1,20)]
        [Required]
        public int Number { get; set; }
    }
}