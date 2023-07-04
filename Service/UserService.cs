using JTable_test2.Data;
using JTable_test2.IService;
using JTable_test2.Models;

namespace JTable_test2.Service
{
    public class UserService : IUserService
    {
        private readonly MNTContext context;
        public UserService(MNTContext context)
        {
            this.context = context;
        }

        public bool AddUser(string username, double score, int gender, int IsActive, int IsToggled)
        {
            try
            {
                var lastId = context.Users.Where(a => a.Id != 0).OrderBy(a => a.Id).Last().Id;
                User user = new User() { Id = lastId + 1, Name = username, Score = score, Gender = gender, IsActive = IsActive, IsToggled = IsToggled };
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }
        public bool DeleteUser(int id)
        {
            try
            {
                var user = context.Users.Find(id);
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public User GetUser(int Id)
        {
            try
            {
                var user = context.Users.FirstOrDefault(a => a.Id == Id);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool UpdateUser(int Id, string username, double score, int gender, int IsActive, int IsToggled)
        {
            try
            {
                var user = context.Users.FirstOrDefault(a => a.Id == Id);
                if (user == null)
                {
                    return false;
                }
                user.Name = username;
                user.Score = score;
                user.Gender = gender;
                user.IsActive = IsActive;
                user.IsToggled = IsToggled;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public List<User> GetAllUsers()
        {
            try
            {
                var users = context.Users.Where(a => a.Id != 0).ToList();
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetUsersCount(string? search)
        {
            try
            {
                if (search == null)
                {
                    return context.Users.Count();
                }
                else
                {
                    return  context.Users.Where(a=>a.Name.Contains(search)).Count();
                }
            }
            catch (Exception)
            {
                return -999;
            }
        }
        public int GetFriendsCount(int friendId)
        {
            try
            {
                return context.Friends.Where(a => a.UserId == friendId).Count();
            }
            catch (Exception)
            {
                return -999;
            }
        }
        public List<User> GetUsersPaged(int StartIndex, int PageSize, string sort, string? search = null)
        {
            try
            {
                var users = context.Users.Where(a => a.Id != 0).ToList();
                if (search != null)
                {
                    users = users.Where(a => a.Name.Contains(search)).ToList();
                }
                var ASC = sort.Split(' ')[1] == "ASC";

                if (ASC)
                {
                    users = users.OrderBy(a => a.Name).ToList();
                }
                else
                {
                    users = users.OrderByDescending(a => a.Name).ToList();
                }
                users = users.Skip(StartIndex).Take(PageSize).ToList();
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Toggle(int Id)
        {
            try
            {
                var user = context.Users.Find(Id);
                if (user.IsToggled == 1)
                {
                    user.IsToggled = 0;
                }
                else
                {
                    user.IsToggled = 1;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public List<Friend> GetFriendsPaged(int StartIndex, int PageSize, int friendId)
        {
            try
            {
                var users = context.Friends.Where(a => a.UserId == friendId).Skip(StartIndex).Take(PageSize).ToList();
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
