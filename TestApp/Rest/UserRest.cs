using System;
using System.Collections.Generic;
using System.Linq;
using LokiLogger.WebExtension.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Rest {
	[Route("api/User")]
	[ApiController]
	public class UserRest :LokiController{
		
		public ITestService TestService { get; set; }

		public List<User> Users { get; set; }
		
		public UserRest(ITestService service)
		{
			Users = new List<User>();
			for (int i = 0; i < 5; i++)
			{
				Users.Add(new User()
				{
					Description = $"User {i} Description",
					Password = "1234" + i,
					UserId = "i",
					UserName = "User"+i
				});
			}
			TestService = service;
			
		}
		[HttpGet("All")]
		public ActionResult GetAllUser()
		{
			return Ok(Users);
		}

		[HttpPost("Login")]
		public ActionResult Login([FromBody] LoginModel model)
		{
			if (ModelState.IsValid)
			{
				if (Users.Select(x => x.Password).Contains(model.Password))
				{
					if (Users.Select(x => x.UserName).Contains(model.UserName))
					{
						return Redirect("/SomeWhere");
					}
				}

				return BadRequest("Login failed");
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("Error")]
		public IActionResult ThrowError()
		{
			return CallRest(TestService.Throw);
		}
	}

	public class LoginModel {
		public string Password { get; set; }
		public string UserName { get; set; }
	}
}