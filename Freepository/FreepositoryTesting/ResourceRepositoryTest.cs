using AutoMapper;
using Freepository.Data;
using Freepository.Models;
using Freepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FreepositoryTesting
{
    public class ResourceRepositoryTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ResourceRepository _repository;

        public ResourceRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ResourceRepositoryDatabaseTEST")
                .Options;
            _context = new ApplicationDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Resource, Resource>();
                cfg.CreateMap<ResourceTag, ResourceTag>();
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _repository = new ResourceRepository(_context, mapper);
        }

        [Fact]
        public async Task GetAllResources_ShouldReturnAllResources()
        {
            // Arrange
            var resources = new List<Resource>
            {
                new Resource { Id = 1 },
                new Resource { Id = 2 }
            };
            _context.Resources.AddRange(resources);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllResources();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetResourceById_ShouldReturnResource()
        {
            // Arrange
            var resource = new Resource { Id = 1 };
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetResourceById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddResource_ShouldAddResource()
        {
            // Arrange
            var resource = new Resource { Id = 1 };

            // Act
            await _repository.AddResource(resource);
            var result = await _context.Resources.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateResource_ShouldUpdateResource()
        {
            // Arrange
            var resource = new Resource { Id = 1, UserId = "1" };
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            resource.UserId = "1";

            // Act
            await _repository.UpdateResource(resource);
            var result = await _context.Resources.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("2", result.UserId);
        }

        [Fact]
        public async Task DeleteResource_ShouldDeleteResource()
        {
            // Arrange
            var resource = new Resource { Id = 1 };
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteResource(1);
            var result = await _context.Resources.FindAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExistResource_ShouldReturnTrue_WhenResourceExists()
        {
            // Arrange
            var resource = new Resource { Id = 1 };
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.ExistResource(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistResource_ShouldReturnFalse_WhenResourceDoesNotExist()
        {
            // Act
            var result = await _repository.ExistResource(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AssignTag_ShouldAssignTagsToResource()
        {
            // Arrange
            var resource = new Resource { Id = 1, ResourceTags = new List<ResourceTag>() };
            var tags = new List<int> { 1, 2 };
            _context.Resources.Add(resource);
            _context.Tags.Add(new Tag { Id = 1 });
            _context.Tags.Add(new Tag { Id = 2 });
            await _context.SaveChangesAsync();

            // Act
            await _repository.AssignTag(1, tags);
            var result = await _context.Resources
                .Include(r => r.ResourceTags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ResourceTags.Count);
        }
    }
}
