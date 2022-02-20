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
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
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
        [MapToApiVersion("1")]
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

        /// <summary>
        /// Gets users which belong to the specified role.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Created user data</returns>
        /// <response code="201">Returns created user</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([Required][FromBody] UserDto dto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                };
                await _userManager.CreateAsync(user);
                return CreatedAtAction(
                    nameof(GetUserByIdAsync),
                    new { id = user.Id }, user
                );
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

        /// <summary>
        /// Gets user by using user ID.
        /// </summary>
        /// <param name="id">User ID to get</param>
        /// <returns>User information</returns>
        /// <response code="200">Returns user</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByIdAsync([Required][FromRoute] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return Ok(user);
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
