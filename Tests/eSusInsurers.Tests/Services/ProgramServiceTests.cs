using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Programs;
using eSusInsurers.Models.Common;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Common.Exceptions;

public class ProgramServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IProgramRepository> _mockProgramRepository;
    private readonly ProgramService _service;

    public ProgramServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _mockProgramRepository = new Mock<IProgramRepository>();
        _mockUnitOfWork.SetupGet(u => u.ProgramRepository).Returns(_mockProgramRepository.Object);
        _service = new ProgramService(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetPrograms_ReturnsPagedResult()
    {
        // Arrange
        var queryable = new List<Program>
        {
            new Program { Id = 1, ProgramName = "Test", IsActive = true }
        }.AsQueryable();

        var programsModel = new ProgramsModel { ProgramId = 1, ProgramName = "Test", IsActive = true };
        var pagedResult = new PagedResult<ProgramsModel>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecordCount = 1,
            Records = new List<ProgramsModel> { programsModel }
        };

        _mockProgramRepository.Setup(r => r.GetAll(It.IsAny<string[]>())).Returns(queryable);
        _mockMapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => { }));

        // ProjectTo is used, so we need to mock it. We'll just return the model as IQueryable.
        var projected = new List<ProgramsModel> { programsModel }.AsQueryable();
        // Simulate ToPagedResult extension method
        // We'll use a helper extension for this test
        _mockMapper.Setup(m => m.ProjectTo<ProgramsModel>(It.IsAny<IQueryable<Program>>(), It.IsAny<object>())).Returns(projected);

        // Act
        var result = await _service.GetPrograms(new GetProgramsQuery
        {
            pagingOptions = new PagingOptions { Page = 1, PageSize = 10 },
            filter = null,
            sortingOptions = null
        }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Records);
        Assert.Equal(1, result.TotalRecordCount);
    }

    [Fact]
    public async Task AddProgram_ShouldAdd_WhenProgramDoesNotExist()
    {
        // Arrange
        var request = new ProgramRequest { ProgramName = "NewProgram" };
        var programEntity = new Program { Id = 1, ProgramName = "NewProgram" };
        var mockTransaction = new Mock<IDbContextTransaction>();

        _mockProgramRepository.Setup(r => r.GetAll()).Returns(new List<Program>().AsQueryable());
        _mockMapper.Setup(m => m.Map<Program>(request)).Returns(programEntity);
        _mockUnitOfWork.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockTransaction.Object);

        // Act
        var result = await _service.AddProgram(request, CancellationToken.None);

        // Assert
        Assert.True(result);
        _mockProgramRepository.Verify(r => r.AddAsync(It.IsAny<Program>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddProgram_ShouldThrow_WhenProgramExists()
    {
        // Arrange
        var request = new ProgramRequest { ProgramName = "ExistingProgram" };
        var existing = new Program { Id = 1, ProgramName = "ExistingProgram" };
        var queryable = new List<Program> { existing }.AsQueryable();

        var mockDbSet = new Mock<DbSet<Program>>();
        _mockProgramRepository.Setup(r => r.GetAll()).Returns(queryable);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.AddProgram(request, CancellationToken.None)
        );
    }

    [Fact]
    public async Task UpdateProgram_ShouldUpdate_WhenValid()
    {
        // Arrange
        var programId = 1;
        var request = new ProgramRequest { ProgramName = "UpdatedProgram" };
        var existing = new Program { Id = programId, ProgramName = "OldProgram" };
        var mockTransaction = new Mock<IDbContextTransaction>();

        _mockProgramRepository.Setup(r => r.GetAll()).Returns(new List<Program> { existing }.AsQueryable());
        _mockProgramRepository.Setup(r => r.GetByIdAsync(programId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        _mockUnitOfWork.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockTransaction.Object);

        // Act
        await _service.UpdateProgram(programId, request, CancellationToken.None);

        // Assert
        _mockProgramRepository.Verify(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateProgram_ShouldThrow_WhenDuplicateName()
    {
        // Arrange
        var programId = 1;
        var request = new ProgramRequest { ProgramName = "DuplicateProgram" };
        var existing = new Program { Id = 2, ProgramName = "DuplicateProgram" };
        var queryable = new List<Program> { existing }.AsQueryable();

        _mockProgramRepository.Setup(r => r.GetAll()).Returns(queryable);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() =>
            _service.UpdateProgram(programId, request, CancellationToken.None)
        );
    }

    [Fact]
    public async Task UpdateProgram_ShouldThrow_WhenNotFound()
    {
        // Arrange
        var programId = 1;
        var request = new ProgramRequest { ProgramName = "NotFound" };
        _mockProgramRepository.Setup(r => r.GetAll()).Returns(new List<Program>().AsQueryable());
        _mockProgramRepository.Setup(r => r.GetByIdAsync(programId, It.IsAny<CancellationToken>())).ReturnsAsync((Program)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateProgram(programId, request, CancellationToken.None)
        );
    }

    [Fact]
    public async Task DeleteProgram_ShouldSetIsActiveFalse_WhenFound()
    {
        // Arrange
        var programId = 1;
        var program = new Program { Id = programId, IsActive = true };
        _mockProgramRepository.Setup(r => r.GetByIdAsync(programId, It.IsAny<CancellationToken>())).ReturnsAsync(program);

        // Act
        await _service.DeleteProgram(programId, CancellationToken.None);

        // Assert
        Assert.False(program.IsActive);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProgram_ShouldThrow_WhenNotFound()
    {
        // Arrange
        var programId = 1;
        _mockProgramRepository.Setup(r => r.GetByIdAsync(programId, It.IsAny<CancellationToken>())).ReturnsAsync((Program)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.DeleteProgram(programId, CancellationToken.None)
        );
    }

    [Fact]
    public async Task ActivateProgram_ShouldSetIsActiveTrue_WhenFound()
    {
        // Arrange
        var programId = 1;
        var program = new Program { Id = programId, IsActive = false };
        _mockProgramRepository.Setup(r => r.GetByIdAsync(programId, It.IsAny<CancellationToken>())).ReturnsAsync(program);

        // Act
        await _service.ActivateProgram(programId, CancellationToken.None);

        // Assert
        Assert.True(program.IsActive);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ActivateProgram_ShouldThrow_WhenNotFound()
    {
        // Arrange
        var programId = 1;
        _mockProgramRepository.Setup(r => r.GetByIdAsync(programId, It.IsAny<CancellationToken>())).ReturnsAsync((Program)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.ActivateProgram(programId, CancellationToken.None)
        );
    }
}