using ContactPro.Models;

namespace ContactPro.Services.Interfaces
{
    public interface IAddressBookService
    {
        Task AddContactToCatagoryAsynce(int categoryID, int contactId);
        Task<bool> IsContactInCategory(int categoryID, int contactId);
        Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId);
        Task<ICollection<Category>> GetContactCategoryIdsAsync(int contactId);
        Task<ICollection<Category>> GetContactCategoriesAsync(int contactId);
        Task RemoveContactFromCategoryAsync(int categoryID, int contactId);
        IEnumerable<Contact> SerachForContacts(string searchString, string userId);

    }
}
