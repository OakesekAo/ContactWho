using ContactPro.Models;

namespace ContactPro.Services.Interfaces
{
    public interface IAddressBookService
    {
        Task AddContactToCatagoryAsynce(int categoryId, int contactId);
        Task<bool> IsContactInCategory(int categoryId, int contactId);
        Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId);
        Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId);
        Task<ICollection<Category>> GetContactCategoriesAsync(int contactId);
        Task RemoveContactFromCategoryAsync(int categoryId, int contactId);
        IEnumerable<Contact> SerachForContacts(string searchString, string userId);

    }
}
