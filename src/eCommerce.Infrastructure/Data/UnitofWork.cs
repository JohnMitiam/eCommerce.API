using eCommerce.Application.Interfaces.Data;

namespace eCommerce.Infrastructure.Data
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DatabaseSession _dbSession;
        private readonly IProductRepository _productRepository;

        public UnitofWork(DatabaseSession dbSession,
            IProductRepository productRepository)
        {
            _dbSession = dbSession;
            _productRepository = productRepository;
        }

        public IProductRepository Products => _productRepository;

        public void Commit()
        {
            if (_dbSession.Transaction != null)
                _dbSession.Transaction.Commit();
            Dispose();
        }

        public void CreateTransaction()
        {
            if (_dbSession.Connection != null)
            {
                _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
            }
            else
            {
                throw new Exception("Database Session is null");
            }
        }

        public void Dispose()
        {
            _dbSession.Transaction?.Dispose();
        }

        public void Rollback()
        {
            if (_dbSession.Transaction != null)
                _dbSession.Transaction.Rollback();
            Dispose();
        }
    }
}
