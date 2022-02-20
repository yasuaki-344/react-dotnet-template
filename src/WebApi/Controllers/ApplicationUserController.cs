using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using ApplicationCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Entities;

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
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationUserController> _logger;

        /// <summary>
        /// Initializes a new instance of ApplicationUserController class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="mapper">O/R mapper object</param>
        /// <param name="logger">Logging object</param>
        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper,
            ILogger<ApplicationUserController> logger
        )
        {
            _userManager = userManager;
            _mapper = mapper;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsersAsync([Required][FromQuery] string role)
        {
            try
            {
                var users = await _userManager.GetUsersInRoleAsync(role);
                var userDto = await Task.WhenAll(users.Select(async (x) => {
                    var dto = _mapper.Map<UserDto>(x);
                    var roles = await _userManager.GetRolesAsync(x);
                    dto.Role = roles.First() ?? string.Empty;
                    return dto;
                }));
                return Ok(userDto);
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
        /// <response code="400">Failed to create user</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([Required][FromBody] UserDto dto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, dto.Role);
                    var userDto = _mapper.Map<UserDto>(user);
                    userDto.Role = dto.Role;
                    return CreatedAtAction(
                        nameof(GetUserByIdAsync),
                        new { id = userDto.Id }, userDto
                    );
                }
                else
                {
                    return BadRequest();
                }
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
        /// <response code="400">Route parameter is wrong</response>
        /// <response code="404">The specified user is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ActionName(nameof(GetUserByIdAsync))]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByIdAsync([Required][FromRoute] string? id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest();
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var dto = _mapper.Map<UserDto>(user);
                        var roles = await _userManager.GetRolesAsync(user);
                        dto.Role = roles.First() ?? string.Empty;
                        return Ok(dto);
                    }
                }
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
        /// Updates user which belong to the specified role.
        /// </summary>
        /// <param name="id">ID of the user to be updated</param>
        /// <param name="dto"></param>
        /// <returns>Updated user data</returns>
        /// <response code="201">Returns updated user</response>
        /// <response code="400">Failed to create user</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserAsync(
            [Required][FromRoute] string id,
            [Required][FromBody] UserDto dto
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return NotFound($"Not found specified user ID: {id}");
                }
                else
                {
                    user.UserName = dto.UserName;
                    user.Email = dto.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, roles);
                        await _userManager.AddToRoleAsync(user, dto.Role);

                        var userDto = _mapper.Map<UserDto>(user);
                        var newRoles = await _userManager.GetRolesAsync(user);
                        userDto.Role = newRoles.First() ?? string.Empty;
                        return CreatedAtAction(
                            nameof(GetUserByIdAsync),
                            new { id = userDto.Id }, userDto
                        );
                    }
                    else
                    {
                        return BadRequest(result.Errors.First().ToString());
                    }
                }
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
        /// Deletes user by using user ID.
        /// </summary>
        /// <param name="id">User ID to delete</param>
        /// <response code="204">The specified user is deleted</response>
        /// <response code="400">User data is not found</response>
        /// <response code="404">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserAsync([Required][FromRoute] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return NotFound($"Not found specified user ID: {id}");
                }
                else
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest(result.Errors.First().ToString());
                    }
                }
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
