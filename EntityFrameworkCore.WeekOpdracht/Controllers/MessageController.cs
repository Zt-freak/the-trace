using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCore.WeekOpdracht.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageService messageService, ILogger<MessageController> logger)
        {
            this.messageService = messageService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(Message message)
        {
            try
            {
                _logger.LogInformation($"saving message");
                return Ok(messageService.Add(message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation($"fetching message");
                return Ok(messageService.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            try
            {
                _logger.LogInformation($"fetching messages");
                return Ok(messageService.Get(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
