// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace Adverts.Data.Entities
{
    [Index(nameof(adv_name), IsUnique = true)]
    public class Advertisers
    {
        [Key]
        public int adv_id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string adv_name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string adv_organisationNumber { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string adv_deliveryAddress { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string adv_postalCode { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string adv_billingAddress { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(50)]
        [RegularExpression(@"^[^åäöÅÄÖ]+$", ErrorMessage = "Invalid characters")]
        public string adv_City { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "Not a valid Phone number")]
        public string adv_PhoneNumber { get; set; }
    }
}
