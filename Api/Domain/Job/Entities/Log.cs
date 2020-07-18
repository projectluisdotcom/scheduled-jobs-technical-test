using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }
        public JobState From { get; set; }
        public JobState To { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}