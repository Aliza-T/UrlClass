using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlRepository
{
    public class UrlRepository
    {
        private string _connectionString;
        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        ////public void AddUser(User user, string password)
        //{

        //    user.PasswordSalt = PasswordHelper.GenerateSalt();
        //    user.PasswordHash = PasswordHelper.HashPassword(password, user.PasswordSalt);
        //    using (var context = new TaskDataContextDataContext(_connectionString))
        //    {
        //        context.Users.InsertOnSubmit(user);
        //        context.SubmitChanges();
        //    }

        //}
        //public User LogIn(string email, string password)
        //{
        //    var user = GetByEmail(email);
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    bool isCorrectPassword = PasswordHelper.PasswordMatch(password, user.PasswordSalt, user.PasswordHash);
        //    if (isCorrectPassword)
        //    {
        //        return user;
        //    }
        //    return null;
        //}
        //public User GetByEmail(string email)
        //{
        //    using (var context = new TaskDataContextDataContext(_connectionString))
        //        return context.Users.FirstOrDefault(e => e.Email == email);
        //}
       // public IEnumerable<url> UrlsForUser(int id)
       // {
            //return all the urls for that user along with the views saved on each one
       // }
    }
  
}
