using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities {
    public class Owner : IEntity {
        public int Id { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        [Display(Name = "Owner Name")]
        [MaxLength(50)]
        public string OwnerName { get; set; }

        [Display(Name = "Fixed Phone")]
        [MinLength(9, ErrorMessage = "Fixed Phone must have 9 digits.")]
        [MaxLength(9)]
        public string FixedPhone { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        [MinLength(9, ErrorMessage = "Cell Phone must have 9 digits.")]
        [MaxLength(9)]
        public string CellPhone { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
