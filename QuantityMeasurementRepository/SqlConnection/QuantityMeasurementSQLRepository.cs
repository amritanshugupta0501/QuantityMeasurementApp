using System;
using System.Data;
using Microsoft.Data.SqlClient;
using QuantityMeasurementModel;

namespace QuantityMeasurementRepository
{
    public class QuantityMeasurementSQLRepository : IQuantityMeasurementRepo
    {
        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=QuantityMeasurementSystem;Trusted_Connection=True;TrustServerCertificate=True;";

        public void SaveMeasurement(MeasurementDetails entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("spInsertMeasurement", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Category", entity.MeasurementCategory);
                    command.Parameters.AddWithValue("@Action", entity.MeasurementAction);
                    command.Parameters.AddWithValue("@FirstValue", entity.MeasurementValueFirst);
                    command.Parameters.AddWithValue("@FirstUnit", entity.MeasurementUnitFirst);
                    command.Parameters.AddWithValue("@SecondValue", entity.MeasurementValueSecond);
                    command.Parameters.AddWithValue("@SecondUnit", entity.MeasurementUnitSecond);
                    command.Parameters.AddWithValue("@TargetUnit", entity.TargetMeasurementUnit ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CalculatedResult", entity.CalculatedResult ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ComparisonResult", entity.ComparisonResult ?? (object)DBNull.Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}