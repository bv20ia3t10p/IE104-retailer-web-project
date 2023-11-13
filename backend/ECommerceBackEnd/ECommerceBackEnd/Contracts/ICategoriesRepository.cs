using ECommerceBackEnd.Entities;

namespace ECommerceBackEnd.Contracts
{
    public interface ICategoriesRepository
    {
        void CreateCategory(Category newCategory);
        void DeleteCategory(int CID);
        IEnumerable<Category> GetCategories();
        Category GetCategory(int CID);
        void UpdateCategory(Category category);
    }
}