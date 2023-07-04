using JTable_test2.IService;
using JTable_test2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace JTable_test2.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonSerializerOptions options;
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Friend(int userid)
        {
            ViewData["Id"] = userid;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult User()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UpdateUser(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "ERROR",
                        Message = "Form is not valid! " +
                        "Please correct it and try again."
                    });
                }

                _userService.UpdateUser(user.Id,user.Name,user.Score,user.Gender??0, user.IsActive??0, user.IsToggled??0);
                return Json(new  { Result = "OK" }, options);
            }
            catch (Exception ex)
            {
                return Json(new  { Result = "ERROR", Message = ex.Message }, options);
            }
        }

        [HttpPost]
        public JsonResult UsersList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null, string search = null)
        {
            try
            {
                int userCount = _userService.GetUsersCount(search);
                List<User> users = _userService.GetUsersPaged(jtStartIndex, jtPageSize,jtSorting, search);
                var r = Json(new { Result = "OK", Records = users, TotalRecordCount = userCount },options);
                return r;
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, options);
            }
        }


        [HttpPost]
        public JsonResult DeleteUser(int Id)
        {
            try
            {
                var res = _userService.DeleteUser(Id);
                return Json(new  { Result = "OK"}, options);
            }
            catch (Exception ex)
            {
                return Json(new  { Result = "ERROR", Message = ex.Message }, options);
            }
        }
        [HttpPost]
        public JsonResult CreateUser(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "ERROR",
                        Message = "Form is not valid! " +
                      "Please correct it and try again."
                    });
                }

                var addedUser = _userService.AddUser(user.Name,user.Score,user.Gender??0, user.IsActive ?? 0, user.IsToggled ?? 0);
                return Json(new  { Result = "OK", Record = addedUser }, options);
            }
            catch (Exception ex)
            {
                return Json(new  { Result = "ERROR", Message = ex.Message }, options);
            }
        }

        [HttpGet]
        public JsonResult Toggle(int Id)
        {
            try
            {
                var res = _userService.Toggle(Id);
                return Json(new  { Result = "OK" }, options);
            }
            catch (Exception ex)
            {
                return Json(new  { Result = "ERROR", Message = ex.Message }, options);
            }
        }


        [HttpPost]
        public JsonResult FriendsList(int userId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int userCount = _userService.GetFriendsCount(userId);
                var friends = _userService.GetFriendsPaged(jtStartIndex, jtPageSize,userId);
                var r = Json(new { Result = "OK", Records = friends, TotalRecordCount = userCount }, options);
                return r;
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, options);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}