namespace QuantityMeasurementModel
{
    public class MeasurementRequestDTO
    {
        public string MeasurementCategory{get; set; }
        public MeasurementAction OperationType { get; set; }
        public string MeasurementUnitFirst { get; set; }
        public double MeasurementValueFirst { get; set; }
        public string MeasurementUnitSecond { get; set; }
        public double MeasurementValueSecond { get; set; }
        public string TargetMeasurementUnit { get; set; }
    }
}