using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using MinimalApi.Endpoints;
using NSubstitute;
using System;
using Xunit;

namespace MinimalApiTests;

public class UserEndpoitsTests
{
    private readonly IUserData _userData = Substitute.For<IUserData>();

    [Fact]
    public void GetUser_ReturnUser_WhenUserExists()
    {
        // Arrange
        var id = new Random().Next();
        var user = new UserModel { Id = id, FirstName = "Saul", LastName = "Goodman" };
        _userData.GetUser(Arg.Is(id)).Returns(user);

        // Act
        var result = UserEndpoints.GetUser(_userData, id).Result;

        // Assert
        result.GetOkObjectResultValue<UserModel>().Should().BeEquivalentTo(user);
        result.GetObjectResultStatusCode().Equals(200);
    }
}