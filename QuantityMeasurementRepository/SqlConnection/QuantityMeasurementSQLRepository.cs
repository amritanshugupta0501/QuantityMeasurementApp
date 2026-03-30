using System.Linq;
using QuantityMeasurementModel;

namespace QuantityMeasurementRepository
{
    public class QuantityMeasurementSQLRepository : IQuantityMeasurementRepo
    {
        private readonly QuantityMeasurementDbContext _context;
        public QuantityMeasurementSQLRepository(QuantityMeasurementDbContext context)
        {
            _context = context;
        }

        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();
        }
        public QuantityMeasurementEntity[] GetAllMeasurements()
        {
            return _context.QuantityMeasurements.ToArray();
        }
        public QuantityMeasurementEntity GetMeasurementById(int id)
        {
            return _context.QuantityMeasurements.Find(id);
        }
        public bool DeleteMeasurement(int id)
        {
            var entity = _context.QuantityMeasurements.Find(id);
            if (entity != null)
            {
                _context.QuantityMeasurements.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}