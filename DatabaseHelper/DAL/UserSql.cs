using DatabaseHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.DAL
{
    public static class UserSql
    {

        public static string GetUserNameById(int userId)
        {
            using (var db = new HotelsContext())
            {
                var result = db.Users
                    .Where(x => x.UserId == userId)
                    .Select(x => x.UserName)
                    .FirstOrDefault();

                return result;
            }
        }

        public static User GetUserById(int userId)
        {
            using (var db = new HotelsContext())
            {
                var result = db.Users
                    .Where(x => x.UserId == userId)
                    .FirstOrDefault();

                if (result == null)
                    throw new Exception($"There is no User with id {userId}");

                return result;
            }
        }
    }
}
