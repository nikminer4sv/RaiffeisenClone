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

public class DepositServiceTests {
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    public DepositServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = new MapperConfiguration(expression => { expression.AddProfile<DepositProfile>(); }).CreateMapper();
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundException()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Deposits.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await depositService.GetByIdAsync(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async Task GetById_ReturnDeposit()
    {
        // ARRANGE
        var userId = Guid.Parse("ca28aedd-bb91-4d29-829a-1faa47ddeef2");
        var deposit = new Deposit() { UserId = userId };
        var depositViewModel = _mapper.Map<DepositViewModel>(deposit);
        _unitOfWorkMock.Setup(m => m.Deposits.GetByIdAsync(deposit.Id)).ReturnsAsync(deposit);
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT
        var depositFromService = await depositService.GetByIdAsync(deposit.Id, userId);

        // ASSERT
        Assert.Equal(depositViewModel.Bid, depositFromService.Bid);
        Assert.Equal(depositViewModel.Term, depositFromService.Term);
        Assert.Equal(depositViewModel.Currency, depositFromService.Currency);
        Assert.Equal(depositViewModel.IsWithdrawed, depositFromService.IsWithdrawed);
        Assert.Equal(depositViewModel.IsReplenished, depositFromService.IsReplenished);
    }

    [Fact]
    public async Task AddAsync_Verify()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Deposits.AddAsync(It.IsAny<Deposit>()));
        _unitOfWorkMock.Setup(m => m.SaveAsync());
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT
        var id = Guid.NewGuid();
        var depositId = await depositService.AddAsync(_mapper.Map<DepositViewModel>(new Deposit()), id);

        // ASSERT
        _unitOfWorkMock.Verify(m => m.Deposits.AddAsync(It.IsAny<Deposit>()));
        Assert.Equal((new Deposit()).Id, depositId);
    }

    [Fact]
    public async Task DeleteAsync_ThrowsKeyNotFound()
    {
        _unitOfWorkMock.Setup(m => m.Deposits.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await depositService.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));
    }

    [Fact]
    public async Task DeleteAsync_Verify()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Deposits.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Deposit());
        _unitOfWorkMock.Setup(m => m.Deposits.DeleteAsync(It.IsAny<Deposit>()));
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT
        await depositService.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>());

        // ASSERT
        _unitOfWorkMock.Verify(m => m.Deposits.DeleteAsync(It.IsAny<Deposit>()));
        _unitOfWorkMock.Verify(m => m.SaveAsync());
    }

    [Fact]
    public async Task UpdateAsync_ThrowsKeyNotFound()
    {
        _unitOfWorkMock.Setup(m => m.Deposits.ContainsAsync(It.IsAny<Deposit>())).ReturnsAsync(() => false);
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT + ASSERT
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await depositService.UpdateAsync(_mapper.Map<DepositUpdateViewModel>(It.IsAny<Deposit>()), It.IsAny<Guid>()));
    }

    [Fact]
    public async Task UpdateAsync_Verify()
    {
        // ARRANGE
        _unitOfWorkMock.Setup(m => m.Deposits.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Deposit() {UserId = Guid.Empty});
        _unitOfWorkMock.Setup(m => m.Deposits.UpdateAsync(It.IsAny<Deposit>()));
        var depositService = new DepositService(_unitOfWorkMock.Object, _mapper, new EmailSender());

        // ACT
        await depositService.UpdateAsync(new DepositUpdateViewModel(), Guid.Empty);

        // ASSERT
        _unitOfWorkMock.Verify(m => m.Deposits.UpdateAsync(It.IsAny<Deposit>()));
        _unitOfWorkMock.Verify(m => m.SaveAsync());
    }
}