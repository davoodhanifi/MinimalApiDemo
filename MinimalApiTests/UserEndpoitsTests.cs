using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using MinimalApi.Endpoints;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MinimalApiTests;

public class UserEndpoitsTests
{
    private readonly Mock<IUserData> _userData;

    public UserEndpoitsTests()
    {
        _userData = new Mock<IUserData>();
    }

    [Fact]
    public void GetUser_ReturnUser_WhenUserExists()
    {
        // Arrange
        var id = new Random().Next();
        var user = new UserModel { Id = id, FirstName = "Saul", LastName = "Goodman" };
        _userData.Setup(usr => usr.GetUser(id)).Returns(Task.FromResult(user));

        // Act
        var result = UserEndpoints.GetUser(_userData.Object, id).Result;

        // Assert
        result.GetOkObjectResultValue<UserModel>().Should().BeEquivalentTo(user);
        result.GetObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public void GetUser_RetrunUser_WhenUserNotExists()
    {
        // Arrange
        _userData.Setup(usr => usr.GetUser(It.IsAny<int>())).Returns(Task.FromResult((UserModel)null));

        // Act
        var result = UserEndpoints.GetUser(_userData.Object, new Random().Next()).Result;

        //Assert
        result.GetObjectResultStatusCode().Should().Be(404);
    }

    [Fact]
    public void InsertUser_ReturnCreatedStatus_WhenUserIsValid()
    {
        // Arrange
        var user = new UserModel { FirstName = "Reza", LastName = "Sadeghi" };

        // Act
        var result = UserEndpoints.InsertUser(_userData.Object, user).Result;

        //Assert
        result.GetObjectResultStatusCode().Should().Be(201);
    }

    [Fact]
    public void IsertUser_RetrunBadRequest_WhenUserIsNotValid()
    {
        // Arrange
        var user = new UserModel { FirstName = "John" };

        // Act
        var result = UserEndpoints.InsertUser(_userData.Object, user).Result;

        // Assert
        result.GetObjectResultStatusCode().Should().Be(400);
    }

    [Fact]
    public void UpdateUser_ReturnOkStatus_WhenUserIsValid()
    {
        // Arrange
        var id = new Random().Next();
        var user = new UserModel { Id = id, FirstName = "Saul", LastName = "Goodman" };
        _userData.Setup(usr => usr.GetUser(id)).Returns(Task.FromResult(user));

        // Act
        var updatedUser = new UserModel { Id = id, FirstName = "Ebi", LastName = "Hamedi" };
        var result = UserEndpoints.UpdateUser(_userData.Object, updatedUser).Result;

        //Assert
        result.GetObjectResultStatusCode().Should().Be(200);
    }

}