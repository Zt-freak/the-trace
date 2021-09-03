using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCore.WeekOpdracht.Console
{
    public class ConsoleApplication
    {
        private static IUserService _userService;
        private static IMessageService _messageService;
        private readonly ILogger _logger;

        public ConsoleApplication(ILogger<ConsoleApplication> logger, IUserService userService, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
            _userService = userService;
        }

        internal void Run()
        {
            _logger.LogInformation("Hewwo wowld UwU!");

            System.Console.WriteLine("Deleting old stuff");
            DeleteAll();

            System.Console.WriteLine("Creating new user");
            var user = CreateUser();

            System.Console.WriteLine("Creating a message from the new user");
            CreateMessage(user);

            System.Console.WriteLine("Basic test succeeded.");
            System.Console.ReadKey();
        }

        private static void CreateMessage(User user)
        {
            var message = _messageService.Add(new Business.Entities.Message
            {
                Content = "Test bericht",
                SenderId = user.Id,
                Title = $"Nieuw test bericht van {user.Lastname}"
            });

            System.Console.WriteLine($"    Message with title {message.Title} created. New ID is {message.Id}");
            Thread.Sleep(2500);
        }

        private static User CreateUser()
        {
            var user = _userService.Add(new Business.Entities.User
            {
                Email = "Test@test.nl",
                Lastname = "De Tester",
                Name = "Test"
            });

            System.Console.WriteLine($"    User with name {user.Name} created. New ID is {user.Id}");
            Thread.Sleep(2500);

            return user;
        }
        private static void DeleteAll()
        {
            foreach (var item in _userService.GetAll().ToList())
                _userService.Delete(item.Id);

            foreach (var item in _messageService.GetAll().ToList())
                _messageService.Delete(item.Id);
        }
    }
}
