using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class UserService : IUserService
    {
        private readonly DataContext context;

        public UserService()
        {
            context = new DataContext();
        }

        public User Add(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            var userContext = context.Set<User>();
            var exists = userContext.Any(x=>x.Email == user.Email);

            if (exists)
                throw new System.Exception("User already exists with given e-mail");

            userContext.Add(user);
            context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var entity = context.Set<User>().Single(x => x.Id == id);
            context.Set<User>().Remove(entity);
            context.SaveChanges();
        }

        public User Get(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException(nameof(id));

            return context.Set<User>().Single(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Set<User>().ToList();
        }
    }
}
