using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<ApplicationUserController> _logger;


        /// <summary>
        /// Initializes a new instance of ApplicationUserController class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="logger">Logging object</param>
        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<ApplicationUserController> logger
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets users which belong to the specified role.
        /// </summary>
        /// <param name="role">Account role to search</param>
        /// <returns>User list</returns>
        /// <response code="200">Returns user list</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/v1/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsersAsync([Required][FromQuery] string role)
        {
            try
            {
                var users = await _userManager.GetUsersInRoleAsync(role);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database"
                );
            }
        }
    }
}
