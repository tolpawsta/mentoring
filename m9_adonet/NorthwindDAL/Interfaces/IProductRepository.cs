using System.Collections;
using System.Collections.Generic;
using NorthwindDAL.Entities;

namespace NorthwindDAL.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}