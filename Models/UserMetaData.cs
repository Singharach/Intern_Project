using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileRap_Process.Models
{
    public class UserMetaData
    {
        [Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
    [ModelMetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [NotMapped]
        public bool KeepLogin { get; set; }
		[NotMapped]
		public string ConfirmPassword { get; set; }
    }
}
