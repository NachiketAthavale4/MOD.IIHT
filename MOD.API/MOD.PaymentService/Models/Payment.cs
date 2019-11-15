using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.PaymentService.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("txtType")]
        [Column(TypeName = "varchar(50)")]
        public string TxtType { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("remarks")]
        [Column(TypeName = "varchar(50)")]
        public string Remarks { get; set; }

        [JsonProperty("mentorId")]
        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        [JsonProperty("mentorName")]
        [Column(TypeName = "varchar(50)")]
        public string MentorName { get; set; }

        [JsonProperty("trainingId")]
        [ForeignKey("Training")]
        public int TrainingId { get; set; }

        [JsonProperty("skillName")]
        [Column(TypeName = "varchar(50)")]
        public string SkillName { get; set; }

        [JsonProperty("totalAmountToMentor")]
        public float TotalAmountToMentor { get; set; }

        public User Mentor { get; set; }

        public Training Training { get; set; }
    }
}
