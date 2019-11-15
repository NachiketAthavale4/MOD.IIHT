using MOD.TrainingService.CustomAttributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOD.TrainingService.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(180)")]
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(50)")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(50)")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("contactNumber")]
        public long ContactNumber { get; set; }
        [Column(TypeName = "varchar(180)")]
        [JsonProperty("registrationCode")]
        public string RegistrationCode { get; set; }
        [RoleAttribute]
        [Column(TypeName = "varchar(1)")]
        [Required]
        [JsonProperty("role")]
        public string Role { get; set; }
        [Column(TypeName = "varchar(150)")]
        [JsonProperty("linkedInUrl")]
        public string LinkedInUrl { get; set; }
        [JsonProperty("yearsOfExperience")]
        public int YearsOfExperience { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("confirmedSignUp")]
        public bool ConfirmedSignUp { get; set; }
        [JsonProperty("resetPasswordDate")]
        public DateTime ResetPasswordDate { get; set; }
        [JsonProperty("resetPassWord")]
        public bool ResetPassword { get; set; }

        public Training Training { get; set; }
        public User()
        {
            this.Active = false;
            this.ConfirmedSignUp = false;
            this.ResetPassword = false;
        }
    }
}
