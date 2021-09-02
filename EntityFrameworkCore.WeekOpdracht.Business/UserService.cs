using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class UserService : IUserService
    {
        private readonly DataContext context;
        private readonly ILogger<UserService> _logger;


        public UserService(ILogger<UserService> logger)
        {
            context = new DataContext();
            _logger = logger;
        }

        public User Add(User user)
        {
            _logger.LogInformation($"adding user");

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userContext = context.Set<User>();
            var exists = userContext.Any(x=>x.Email == user.Email);

            if (exists)
            {
                _logger.LogError($"User already exists with given e-mail: {user.Email}");

                throw new System.Exception("User already exists with given e-mail");
            }
                
            userContext.Add(user);
            _logger.LogInformation($"saving user");
            context.SaveChanges();
            _logger.LogInformation($"user saved");

            return user;
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"yeeting user");
            var entity = context.Set<User>().Single(x => x.Id == id);
            context.Set<User>().Remove(entity);
            context.SaveChanges();
            _logger.LogInformation($"user yeeted");

        }

        public User Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation($"fetching user with id {id} failed");

                throw new ArgumentNullException(nameof(id));
            }
                
            _logger.LogInformation($"fetching user");
            return context.Set<User>().Single(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Set<User>().ToList();
        }
    }
}
