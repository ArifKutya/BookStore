using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
   public interface IUserService
{
    Task<UserModel> GetUserByIdAsync(string userId);
}

public class UserService : IUserService
{
    private readonly IBookService _bookService;
    private readonly IProductService _productService;

    public UserService(IBookService bookService, IProductService productService)
    {
        _bookService = bookService;
        _productService = productService;
    }

    public async Task<UserModel> GetUserByIdAsync(string userId)
    {
        var bookId = Guid.NewGuid(); // Примерен идентификатор на книга
        var productId = Guid.NewGuid(); // Примерен идентификатор на продукт

        var book = await _bookService.GetBookByIdAsync(bookId);
        var product = await _productService.GetProductByIdAsync(productId);

        var userModel = new UserModel
        {
            UserId = userId,
            UserName = "John Doe",
            UserProduct = product
        };

        return userModel;
    }
}

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserModel>> GetUserById(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
}
