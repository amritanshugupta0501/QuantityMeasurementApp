namespace QuantityMeasurementModel
{
    public class MeasurementDetails
    {
        public string MeasurementCategory { get; set; }
        public string MeasurementAction { get; set; }
        public double MeasurementValueFirst { get; set; }
        public string MeasurementUnitFirst { get; set; }
        public double MeasurementValueSecond { get; set; }
        public string MeasurementUnitSecond { get; set; }
        public string TargetMeasurementUnit { get; set; }
        public double? CalculatedResult { get;set; }
        public bool? ComparisonResult { get; set; }
    }
}