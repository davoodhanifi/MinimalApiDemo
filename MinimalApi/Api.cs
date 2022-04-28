﻿
namespace MinimalApi;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
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
            return Results.Ok(await userData.GetUser(id));
        }
        catch(Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> InsertUser(IUserData userData, UserModel userModel)
    {
        try
        {
            await userData.Insert(userModel);
            return Results.Ok();
        }
        catch(Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> UpdateUser(IUserData userData, UserModel userModel)
    {
        try
        {
            await userData.Update(userModel);
            return Results.Ok();
        }
        catch(Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }

    public static async Task<IResult> DeleteUser(IUserData userData, int id)
    {
        try
        {
            await userData.Delete(id);
            return Results.Ok();
        }
        catch(Exception exception)
        {
            return Results.Problem(exception.Message);
        }
    }
}
