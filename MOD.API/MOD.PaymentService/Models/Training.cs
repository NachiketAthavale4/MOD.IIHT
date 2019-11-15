using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOD.PaymentService.Models
{
    [Table("Training")]
    public class Training
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        [Column(TypeName = "varchar(60)")]
        public string Status { get; set; }

        [JsonProperty("progress")]
        public int Progress { get; set; }

        [JsonProperty("commisionAmount")]
        public float CommisionAmount { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("averageRating")]
        public float AverageRating { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("startTime")]
        [Column(TypeName = "varchar(60)")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        [Column(TypeName = "varchar(60)")]
        public string EndTime { get; set; }

        [JsonProperty("amountReceived")]
        public float AmountReceived { get; set; }

        [ForeignKey("User")]
        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [Column(TypeName = "varchar(60)")]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [ForeignKey("Mentor")]
        [JsonProperty("mentorId")]
        public int MentorId { get; set; }

        [Column(TypeName = "varchar(60)")]
        [JsonProperty("mentorName")]
        public string MentorName { get; set; }

        [ForeignKey("TrainingTechnology")]
        [JsonProperty("technologyId")]
        public int TechnologyId { get; set; }

        [Column(TypeName = "varchar(60)")]
        [JsonProperty("technologyName")]
        public string TechnologyName { get; set; }

        [JsonProperty("fees")]
        public float Fees { get; set; }

        public Technology Technology { get; set; } 

        public Training()
        {
            this.AverageRating = 0.0f;
        }
    }
}
