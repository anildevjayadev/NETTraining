using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _repository;
        public UserController(IUser repository)
        {
            _repository = repository;
        }

        [HttpGet("login")]
        public IActionResult Login(string email, string password)
        {
            var user = _repository.GetUserByEmail(email);
            if (user == null)
                return NotFound();
            if (user.Password != password)
                return Unauthorized();
            return StatusCode(200, "User login successful");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _repository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            var existingUser = _repository.GetUserById(user.UserID);
            if (existingUser != null)
                return BadRequest("User ID already exists!");           
            _repository.AddUser(user);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host
                + HttpContext.Request.Path + "/" + user.UserID, user);
        }

        [HttpGet("{userid}")]
        public IActionResult Get(int userid)
        {
            var requestedUser = _repository.GetUserById(userid);
            if (requestedUser == null)
            {
                return StatusCode(400, "User not available");
            }
            return Ok(requestedUser);
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            var updateUser = _repository.GetUserById(user.UserID);
            if (updateUser == null)
            {
                return NotFound();
            }
            updateUser.UserEmail = user.UserEmail;
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            var data = _repository.UpdateUser(updateUser);

            return Ok(data);
        }      

    }
}
