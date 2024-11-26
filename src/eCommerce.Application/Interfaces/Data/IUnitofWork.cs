using System;
namespace eCommerce.Application.Interfaces.Data
{
    public interface IUnitofWork : IDisposable
    {
        IProductRepository Products {  get; }

        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}
