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
        result.GetObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public void GetUser_RetrunUser_WhenUserNotExists()
    {
        // Arrange
        _userData.GetUser(id: Arg.Any<int>()).Returns((UserModel?)null);

        // Act
        var result = UserEndpoints.GetUser(_userData, new Random().Next()).Result;

        //Assert
        result.GetObjectResultStatusCode().Should().Be(404);
    }

    [Fact]
    public void InsertUser_ReturnCreatedStatus_WhenUserIsValid()
    {
        // Arrange
        var user = new UserModel { FirstName = "Reza", LastName = "Sadeghi" };

        // Act
        var result = UserEndpoints.InsertUser(_userData, user).Result;

        //Assert
        result.GetObjectResultStatusCode().Should().Be(201);
    }

    [Fact]
    public void IsertUser_RetrunBadRequest_WhenUserIsNotValid()
    {
        // Arrange
        var user = new UserModel { FirstName = "John" };

        // Act
        var result = UserEndpoints.InsertUser(_userData, user).Result;

        // Assert
        result.GetObjectResultStatusCode().Should().Be(400);
    }
}