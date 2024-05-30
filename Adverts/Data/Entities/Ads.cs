// <copyright file="Advert.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Adverts.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

   
    public class Ads
    {
        [Key]
        public int ad_id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string ad_content { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string ad_headline { get; set; }

        public double ad_advertismentPrice { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public double ad_productPrice { get; set; }

        [AllowNull]
        [ForeignKey("ad_advertisers_id")]
        public int? ad_advertisers_id { get; set; }

        [AllowNull]
        public virtual Advertisers? ad_Advertisers { get; set; }
    
    }
    
}
