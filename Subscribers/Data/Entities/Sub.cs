using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscribers.Data.Entities
{
    [Index(nameof(sub_FirstName), IsUnique = true)]
    public class Sub
    {
        [Key]
        public int sub_id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string sub_FirstName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string sub_LastName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string sub_deliveryAddress { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "Not a valid Phone number")]
        public string sub_PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string sub_postalCode { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string sub_socialSecurityNumber { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int sub_subscriptionNumber { get; set; }
    }
}
