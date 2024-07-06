using Application.Shared.Entities;
using WebApplication.User.API.DTOs;

namespace WebApplication.User.API.Interfaces;

public interface IUserService : Application.Shared.Abstractions.UserServiceAbstraction.IBaseService<UserDto, 
    Application.Shared.Entities.User>
{
}
