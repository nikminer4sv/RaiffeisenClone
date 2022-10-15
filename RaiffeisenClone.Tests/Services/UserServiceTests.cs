using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using RaiffeisenClone.Application.MappingProfiles;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Tests.Services;

public class UserServiceTests {

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    public UserServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = new MapperConfiguration(expression => { expression.AddProfile<UserProfile>(); }).CreateMapper();
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundException()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Users.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.GetByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task GetById_ReturnUser()
    {
        // ARRANGE
        var userId = Guid.Parse("ca28aedd-bb91-4d29-829a-1faa47ddeef2");
        var user = new User
        {
            Id = userId,
            FirstName = "Nikita",
            LastName = "Korotki",
            DateOfBirth = DateTime.Now,
            Username = "nikminer4sv",
            PasswordHash = "ggwp",
            RefreshTokens = null
        };
        _unitOfWorkMock.Setup(m => m.Users.GetByIdAsync(userId)).ReturnsAsync(user);
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        var userFromService = await userService.GetByIdAsync(userId);

        // ASSERT
        Assert.Equal(user, userFromService);
    }

    [Fact]
    public async Task GetByUsername_ThrowsNotFoundException()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Users.GetByUsernameAsync(It.IsAny<string>()));
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.GetByUsernameAsync("someusername"));
    }

    [Fact]
    public async Task GetByUsername_ReturnUser()
    {
        // ARRANGE
        var username = "nikmienr4sv";
        var user = new User
        {
            Id = Guid.Parse("ca28aedd-bb91-4d29-829a-1faa47ddeef2"),
            FirstName = "Nikita",
            LastName = "Korotki",
            DateOfBirth = DateTime.Now,
            Username = username,
            PasswordHash = "ggwp",
            RefreshTokens = null
        };
        _unitOfWorkMock.Setup(m => m.Users.GetByUsernameAsync(username)).ReturnsAsync(user);
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        var userFromService = await userService.GetByUsernameAsync(username);

        // ASSERT
        Assert.Equal(user, userFromService);
    }

    [Fact]
    public async Task IsUserExist_ReturnsTrue()
    {
        // ARRANGE
        string username = "nikminer4sv";
        _unitOfWorkMock.Setup(m => m.Users.GetByUsernameAsync(username)).ReturnsAsync(new User(){});
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        var result = await userService.IsUserExist(username);

        // ASSERT
        Assert.Equal(result, true);
    }

    [Fact]
    public async Task IsUserExist_ReturnsFalse()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Users.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        var result = await userService.IsUserExist(It.IsAny<string>());

        // ASSERT
        Assert.Equal(result, false);
    }

    [Fact]
    public async Task AddAsync_ThrowsUserAlreadyExists()
    {
        // ARRANGE
        var username = "nikminer4sv";
        var registerViewModel = new RegisterViewModel
        {
            FirstName = "Nikita",
            LastName = "Korotki",
            Username = username,
            DateOfBirth = DateTime.Now,
            Password = "ggwp"
        };
        _unitOfWorkMock.Setup(m => m.Users.GetByUsernameAsync(username)).ReturnsAsync(new User(){Username = username});
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT + ASSERT
        Assert.ThrowsAsync<Exception>(async () => await userService.AddAsync(registerViewModel));
    }

    [Fact]
    public async Task AddAsync_Verify()
    {
        // ARRANGE
        var registerViewModel = new RegisterViewModel
        {
            FirstName = "Nikita",
            LastName = "Korotki",
            Username = "nikminer4sv",
            DateOfBirth = DateTime.Now,
            Password = "7514895263q"
        };
        _unitOfWorkMock.Setup(m => m.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(() => Guid.NewGuid());
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        await userService.AddAsync(registerViewModel);

        // ASSERT
        _unitOfWorkMock.Verify(m => m.Users.AddAsync(It.IsAny<User>()));
    }

    [Fact]
    public async Task DeleteAsync_ThrowsKeyNotFound()
    {
        _unitOfWorkMock.Setup(m => m.Users.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.DeleteAsync(It.IsAny<Guid>()));
    }

    [Fact]
    public async Task DeleteAsync_Verify()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Users.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User() {});
        _unitOfWorkMock.Setup(m => m.Users.DeleteAsync(It.IsAny<User>()));
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        await userService.DeleteAsync(It.IsAny<Guid>());

        // ASSERT
        _unitOfWorkMock.Verify(m => m.Users.DeleteAsync(It.IsAny<User>()));
        _unitOfWorkMock.Verify(m => m.SaveAsync());
    }

    [Fact]
    public async Task UpdateAsync_ThrowsKeyNotFound()
    {
        _unitOfWorkMock.Setup(m => m.Users.ContainsAsync(It.IsAny<User>())).ReturnsAsync(() => false);
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.UpdateAsync(It.IsAny<User>()));
    }

    [Fact]
    public async Task UpdateAsync_Verify()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Users.ContainsAsync(It.IsAny<Guid>())).ReturnsAsync(() => true);
        _unitOfWorkMock.Setup(m => m.Users.UpdateAsync(It.IsAny<User>()));
        var userService = new UserService(_unitOfWorkMock.Object, _mapper);

        // ACT
        await userService.UpdateAsync(new User(){});

        // ASSERT
        _unitOfWorkMock.Verify(m => m.Users.UpdateAsync(It.IsAny<User>()));
        _unitOfWorkMock.Verify(m => m.SaveAsync());
    }
}