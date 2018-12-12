using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlClass.Data
{
    public class UrlRepository
    {
        private string _connectionString;
        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {

            user.PasswordSalt = PasswordHelper.GenerateSalt();
            user.PasswordHash = PasswordHelper.HashPassword(password, user.PasswordSalt);
            using (var context = new UrlShortenerDataContext(_connectionString))
            {
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }

        }
        public User LogIn(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isCorrectPassword = PasswordHelper.PasswordMatch(password, user.PasswordSalt, user.PasswordHash);
            if (isCorrectPassword)
            {
                return user;
            }

            return null;

        }
        public User GetByEmail(string email)
        {
            using (var context = new UrlShortenerDataContext(_connectionString))
                return context.Users.FirstOrDefault(e => e.Email == email);
        }
        public Url GetOriginal(string shortened)
        {
            using (var context = new UrlShortenerDataContext(_connectionString))
                return context.Urls.FirstOrDefault(u => u.ShortenedUrl == shortened);
        }
        public void AddUrl(Url url)
        {
            using (var context = new UrlShortenerDataContext(_connectionString))
            {
                context.Urls.InsertOnSubmit(url);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Url> GetUrls(int id)
        {
            using (var context = new UrlShortenerDataContext(_connectionString))
                return context.Urls.Where(u => u.UserId == id).ToList(); ;
        }
        public Url Check(string Url, string email)
        {
            using (var context = new UrlShortenerDataContext(_connectionString))
                return context.Urls.FirstOrDefault(u => u.OriginalUrl == Url && u.User.Email == email);
        }
        public void IncremementViews(int id)
        {
            using (var context = new UrlShortenerDataContext(_connectionString))
                context.ExecuteCommand("Update Urls Set views = Views + 1 WHERE Id = {0}", id);
        }
    }


}



