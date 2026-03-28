namespace QuantityMeasurementModel
{
    public class QuantityMeasurementDTO
    {
        public double thisValue { get; set; }
        public string thisUnit { get; set; }
        public string thisMeasurementType { get; set; }
        public double thatValue { get; set; }
        public string thatUnit { get; set; }
        public string thatMeasurementType { get; set; }
        public string operation { get; set; }
        public string resultString { get; set; }
        public double resultValue { get; set; }
        public string resultUnit { get; set; }
        public string resultMeasurementType { get; set; }
        public string errorMessage { get; set; }
        public bool error { get; set; }
    }
}