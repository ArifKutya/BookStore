using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public Task<UserInfo?> GetUserInfoAsync(string userName, string password)
        {
            return _userInfoRepository.GetUserInfoAsync(userName, password);
        }

        public async Task Add(AddUserInfoRequest user)
        {
            await _userInfoRepository.Add(new UserInfo()
            {
                Id = Guid.NewGuid(),
                Username = user.Email,
                Password = user.Password,
            });
        }
    }
}
