using web.Entities;
using web.Data;
// using web.Helpers;
using web.Models;
using AutoMapper;
using BCrypt.Net;

namespace web.Services
{
    public interface IUserService 
    {
        public void Register(RegisterModel model);
        public AuthResponse Authenticate(AuthRequest model);
        public List<User> GetUsers();
        public User GetById(int id);
        public void RemoveById(int id);
    }

    public class UserService : IUserService 
    {
        private DatabaseContext context;
        public IMapper mapper;
        public UserService(DatabaseContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void Register(RegisterModel model) {
            if (context.Users.Any(u => u.Email == model.Email)) {
                throw new ApplicationException("Email already exists!");
            }
            var user = mapper.Map<User>(model);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.CreatedAt = DateTime.Now;
            context.Users.Add(user);
            context.SaveChanges();
        }
        public AuthResponse Authenticate(AuthRequest model) {
            var user = context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                throw new ApplicationException("Email or password incorrect!");
            }

            var response = mapper.Map<AuthResponse>(user);
            response.Token = "secret-token" + user.CreatedAt.ToString();
            response.ExpirationDate = DateTime.Now.AddMinutes(15);
            return response;
        }

        public List<User> GetUsers() {
            return context.Users.ToList();
        }

        public User GetById(int id) {
            return context.Users.SingleOrDefault<User>(u => u.Id == id);
        }
        
        public void RemoveById(int id) {
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null) {
                throw new ApplicationException("User with this ID doesn't exist!");
            } 
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}