using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETTraining.Models;

namespace NETTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        List<UserModels> users = new List<UserModels>
        {
            new UserModels{UserID=1,UserEmail="mark.tan@gmail.com",Password="mark@123",FirstName="Mark",LastName="Tan"},
            new UserModels{UserID=2,UserEmail="scott.pan@gmail.com",Password="scott@123",FirstName="Scott",LastName="Pan"},
            new UserModels{UserID=3,UserEmail="tom.schmidt@gmail.com",Password="tom@123",FirstName="Tom",LastName="Schmidt"},
        };

        [HttpPost]
        [Route("login")]
        public ActionResult<UserModels> Login(string userEmail,string password)
        {
            var userDetails = users.FirstOrDefault(x => x.UserEmail == userEmail && x.Password == password);
            if (userDetails == null)
            {
                return Unauthorized();
            }
            return StatusCode(200, "User login successful");
        }

        [HttpGet]
        public List<UserModels> GetUserList()
        {
            return users;
        }

        [Route("{userid}")]
        [HttpGet]
        public ActionResult<UserModels> GetUserByID(int userid)
        {
            var requestedUser = users.FirstOrDefault(u => u.UserID == userid);
            if (requestedUser == null)
            {
                return StatusCode(400, "User not available");
            }
            return requestedUser;
        }

        [HttpPost]
        public ActionResult<UserModels> CreateNewUser(UserModels user)
        {
            if (user.UserID == 0 || user.UserEmail == null || user.FirstName == null || user.Password == null)
            {
                return BadRequest();
            }
            bool userExists = users.Any(p => p.UserEmail == user.UserEmail);
            if (userExists != false)
            {
                return StatusCode(409, "User already exists");
            }
            users.Add(user);
            return Ok();
        }

        [HttpPut]
        public ActionResult<UserModels> UpdateExistingUser(UserModels user)
        {
            var updateUser = users.FirstOrDefault(u => u.UserID == user.UserID);

            if (updateUser == null)
            {
                return NotFound();
            }

            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.UserEmail = user.UserEmail;

            return Ok();
        }


    }
}
