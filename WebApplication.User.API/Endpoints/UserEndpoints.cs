using Application.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using WebApplication.User.API.DTOs;
using WebApplication.User.API.Exceptions;
using WebApplication.User.API.Interfaces;

namespace WebApplication.User.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("newuser", async ([FromBody]UserDto userDto,
            IUserService userService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                if (userDto == null)
                {
                    throw new UserException(nameof(userDto));
                }

                Application.Shared.Response.Result<Application.Shared.Entities.User> result =
                    await userService.CreateAsync(userDto, cancellationToken);

                return result.IsSuccess ?
                    Results.Ok(result) :
                    Results.BadRequest(result);

            }
            catch (UserException) 
            {
                return Results.BadRequest(userDto);
            }
           
        });


        app.MapGet("userbyid/{id}", async ([FromQuery]string guid,
            IUserService userService,
            CancellationToken cancellationToken) =>
        {
            try
            {
                if (Guid.TryParse(guid, out Guid guidResult))
                {
                    Result<Application.Shared.Entities.User> ByIdUser = await userService.GetByIdAsync(guidResult, cancellationToken);

                    return ByIdUser.IsSuccess ?
                        Results.Ok(ByIdUser) : 
                        Results.BadRequest(ByIdUser);
                }

                return Results.Ok(Result<Application.Shared.Entities.User>.Failure(Error.InvalidId + " " + guid));
            }
            catch (UserException)
            {
                return Results.BadRequest(
                    Result<Application.Shared.Entities.User>.Failure(Error.InvalidId + " " + guid));
            }

        });
    }
}
