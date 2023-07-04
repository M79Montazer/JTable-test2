using JTable_test2.Models;

namespace JTable_test2.IService
{
    public interface IUserService
    {
        public bool AddUser(string username, double score, int gender, int IsActive, int IsToggled);
        public bool DeleteUser(int Id);
        public User GetUser(int Id);
        public List<User> GetAllUsers();
        public int GetUsersCount(string? search);
        public int GetFriendsCount(int friendId);
        public List<User> GetUsersPaged(int StartIndex, int PageSize, string sort, string? search);
        public List<Friend> GetFriendsPaged(int StartIndex, int PageSize,int friendId);
        public bool UpdateUser(int Id, string username, double score,int gender, int IsActive, int IsToggled);
        public bool Toggle(int Id);

    }
}
