namespace MinimalApi.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", GetUsers);
        app.MapGet("/users/{id}", GetUser);
        app.MapPost("/users", InsertUser);
        app.MapPut("/users", UpdateUser);
        app.MapDelete("/users/{id}", DeleteUser);
    }

    public static async Task<IResult> GetUsers(IUserData userData)
    {
        try
        {
            return Results.Ok(await userData.GetAll());
        }
        catch (Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> GetUser(IUserData userData, int id)
    {
        try
        {
            var user = await userData.GetUser(id);
            return user != null ? Results.Ok(user) : Results.NotFound();
        }
        catch (Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> InsertUser(IUserData userData, UserModel user)
    {
        try
        {
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                return Results.BadRequest("FirstName or LastName can not be NULL or Empty!");

            await userData.Insert(user);
            return Results.Created($"/users/{user.Id}", user);
        }
        catch (Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> UpdateUser(IUserData userData, UserModel user)
    {
        try
        {
            if (user.Id <= 0)
                return Results.BadRequest("Id must be a valid number!");

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                return Results.BadRequest("FirstName or LastName can not be NULL or Empty!");

            await userData.Update(user);
            return Results.Ok();
        }
        catch (Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> DeleteUser(IUserData userData, int id)
    {
        try
        {
            if (id <= 0)
                return Results.BadRequest("Id must be a valid number!");

            await userData.Delete(id);
            return Results.Ok();
        }
        catch (Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }
}
