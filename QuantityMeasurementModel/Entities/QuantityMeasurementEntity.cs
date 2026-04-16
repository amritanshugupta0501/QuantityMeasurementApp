using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementModel
{
    [Table("MeasurementDataHistory")] 
    public class QuantityMeasurementEntity
    {
        [Key] 
        public int Id { get; set; }
        [Column("Category")]
        public string Category { get; set; }
        [Column("MeasurementAction")]
        public MeasurementOperation MeasurementAction { get; set; }
        [Column("FirstValue")]
        public double FirstValue { get; set; }
        [Column("FirstUnit")]
        public string FirstUnit { get; set; }
        [Column("SecondValue")]
        public double? SecondValue { get; set; }
        [Column("SecondUnit")]
        public string SecondUnit { get; set; }
        [Column("TargetUnit")]
        public string TargetUnit { get; set; }
        [Column("CalculatedResult")]
        public double CalculatedResult { get; set; } 
        [Column("ComparisonResult")]
        public bool? ComparisonResult { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public bool IsError { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
        [NotMapped]
        public string ResultString { get; set; }
    }
}