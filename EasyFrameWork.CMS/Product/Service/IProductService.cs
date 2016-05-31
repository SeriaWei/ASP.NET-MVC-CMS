using Easy.RepositoryPattern;
using Easy.Web.CMS.Product.Models;

namespace Easy.Web.CMS.Product.Service
{
    public interface IProductService : IService<ProductEntity>
    {
        void Publish(long ID);
    }
}