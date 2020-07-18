using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DataPoint
    {
        [Key]
        public Guid Id { get; set; }
        public string Data { get; set; }
        public Job Job { get; set; }
    }
}