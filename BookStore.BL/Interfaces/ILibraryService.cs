using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface ILibraryService
    {
        Task<GetAllBooksByAuthorResponse> 
            GetAllBooksByAuthorId(Guid authorId);
    }
}
