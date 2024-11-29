//using eCommerce.Application.Mappings;
//using eCommerce.Infrastructure.Data;
//using eCommerce.Infrastructure.Data.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace eCommerce.Application.Test.Data
//{
//    public class MockDataFactory
//    {
//        private readonly DatabaseSession _databaseSession;
//        private readonly MockUnitofWork _unitOfWork;
//        private readonly MappingProfile _mapper;
//        public MockDataFactory()
//        {
//            _unitOfWork = new MockUnitofWork(_databaseSession,
//                new ProductRepository(_databaseSession));
//            _mapper = new MappingProfile();
//        }

//        public MockUnitofWork UnitofWork { get { return _unitOfWork; } }

//        public DatabaseSession DatabaseSession { get { return _databaseSession; } }

//        public MappingProfile MappingProfile { get { return _mapper; } }
//    }
//}
