//using eCommerce.Application.Interfaces.Data;
//using eCommerce.Infrastructure.Data;

//namespace eCommerce.Application.Test.Data
//{
//    public class MockUnitofWork : IUnitofWork
//    {
//        private readonly DatabaseSession _dbSession;
//        private readonly IProductRepository _productRepository;
//        public MockUnitofWork(DatabaseSession dbSession, IProductRepository productRepository)
//        {
//            _dbSession = dbSession;
//            _productRepository = productRepository;
//        }

//        public IProductRepository Products => _productRepository;

//        public void Commit()
//        {
//            if (_dbSession.Transaction != null)
//                _dbSession.Transaction.Commit();
//            Dispose();
//        }

//        public void CreateTransaction()
//        {
//            if (_dbSession.Connection != null)
//            {
//                _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
//            }
//            else
//            {
//                throw new Exception("Database Session is null");
//            }
//        }

//        public void Dispose()
//        {
//            _dbSession.Transaction?.Dispose();
//        }

//        public void Rollback()
//        {
//            if (_dbSession.Transaction != null)
//                _dbSession.Transaction.Rollback();
//            Dispose();
//        }
//    }
//}
