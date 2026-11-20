using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TheAbstraction.Application.Commands.User;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces.Auth;
using Xunit;

namespace TheAbstraction.UnitTests.Services.User;

public class User_Service_Tests
{
    [Fact]
    public async Task CreateUser_ReturnsNewUserId()
    {
        var identityService = new Mock<IIdentityService>();
        identityService
            .Setup(service => service.CreateUserAsync(
                "new-user", "password", "new@example.com", "New User", It.IsAny<List<string>>()))
            .ReturnsAsync((true, "user-id"));
        var handler = new CreateUserCommandHandler(identityService.Object);

        var userId = await handler.Handle(new CreateUserCommand
        {
            UserName = "new-user",
            Password = "password",
            ConfirmationPassword = "password",
            Email = "new@example.com",
            FullName = "New User",
            Roles = ["user"]
        }, CancellationToken.None);

        Assert.Equal("user-id", userId);
        identityService.VerifyAll();
    }

    [Fact]
    public async Task CreateUser_ThrowsWhenPasswordsDoNotMatch()
    {
        var identityService = new Mock<IIdentityService>();
        var handler = new CreateUserCommandHandler(identityService.Object);

        await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(new CreateUserCommand
        {
            UserName = "new-user",
            Password = "password",
            ConfirmationPassword = "different-password",
            Email = "new@example.com",
            FullName = "New User"
        }, CancellationToken.None));

        identityService.Verify(service => service.CreateUserAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>()), Times.Never);
    }

    [Fact]
    public async Task UpdateUserRoles_ResolvesUserIdAndReplacesRoles()
    {
        var identityService = new Mock<IIdentityService>();
        identityService.Setup(service => service.GetUserNameAsync("user-id")).ReturnsAsync("new-user");
        identityService.Setup(service => service.UpdateUsersRole("new-user", It.Is<IList<string>>(roles => roles.SequenceEqual(new[] { "admin" }))))
            .ReturnsAsync(true);
        var handler = new UpdateUserRolesCommandHandler(identityService.Object);

        var result = await handler.Handle(new UpdateUserRolesCommand
        {
            UserId = "user-id",
            Roles = ["admin"]
        }, CancellationToken.None);

        Assert.Equal(1, result);
        identityService.VerifyAll();
    }

    [Fact]
    public async Task DeleteUser_ThrowsWhenRequestHasNoAuthenticatedUser()
    {
        var identityService = new Mock<IIdentityService>();
        var contextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
        var handler = new DeleteUserCommandHandler(identityService.Object, contextAccessor);

        await Assert.ThrowsAsync<ForbiddenAccessException>(() => handler.Handle(
            new DeleteUserCommand { UserId = "user-id" }, CancellationToken.None));

        identityService.Verify(service => service.DeleteUserAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task DeleteUser_AllowsUserToDeleteOwnAccount()
    {
        var identityService = new Mock<IIdentityService>();
        identityService.Setup(service => service.GetUserRolesAsync("user-id")).ReturnsAsync([]);
        identityService.Setup(service => service.DeleteUserAsync("user-id")).ReturnsAsync(true);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(ClaimTypes.NameIdentifier, "user-id")], "test"));
        var contextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext { User = principal } };
        var handler = new DeleteUserCommandHandler(identityService.Object, contextAccessor);

        var result = await handler.Handle(new DeleteUserCommand { UserId = "user-id" }, CancellationToken.None);

        Assert.Equal(1, result);
        identityService.VerifyAll();
    }
}
