using Application.Shared.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using WebApplication.User.API.ApplicationDb;
using WebApplication.User.API.DTOs;
using WebApplication.User.API.Exceptions;
using WebApplication.User.API.Interfaces;

namespace WebApplication.User.API.Services;

public class UserService(AppDbContext appDbContext) : IUserService
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<Result<Application.Shared.Entities.User>> CreateAsync(UserDto input, CancellationToken cancellationToken)
    {
        try
        {
            if (input == null) throw new UserException(nameof(input));

            EntityEntry<Application.Shared.Entities.User> addedUser = 
                _appDbContext.Users.Add(new Application.Shared.Entities.User
                {
                    UserId = Guid.NewGuid(),
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    CreateUtc = DateTime.UtcNow,
                    UpdateUtc = DateTime.UtcNow
                });

            await _appDbContext.SaveChangesAsync();

            if (addedUser == null) throw new UserException(nameof(addedUser));

            return Result<Application.Shared.Entities.User>.Success(addedUser.Entity,
                HttpStatusCode.Created);
        }
        catch (UserException)
        {
            return Result<Application.Shared.Entities.User>.Failure(Error.InvalidObject, HttpStatusCode.BadRequest);
        }
    }

    public Task<Result<Application.Shared.Entities.User>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Application.Shared.Entities.User>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {

            Application.Shared.Entities.User? ById = await _appDbContext.Users
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            if (ById == null) throw new UserException($"Invalid {id}");

            return Result<Application.Shared.Entities.User>.Success(ById, HttpStatusCode.OK);

        }
        catch (UserException)
        {
            return Result<Application.Shared.Entities.User>.Failure(Error.InvalidId);
        }
    }

    public Task<Result<Application.Shared.Entities.User>> UpdateAsync(UserDto input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
