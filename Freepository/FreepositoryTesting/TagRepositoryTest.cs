using Freepository.Data;
using Freepository.Models;
using Freepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FreepositoryTesting
{
    public class TagRepositoryTest
    {
        private readonly ApplicationDbContext _context;
        private readonly TagRepository _repository;

        public TagRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TagRepositoryDatabaseTEST") // Nombre de base de datos en memoria
                .Options;
            _context = new ApplicationDbContext(options);

            _repository = new TagRepository(_context);
        }

        [Fact]
        public async Task GetAllTags_ShouldReturnAllTags()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Tag1" },
                new Tag { Id = 2, Name = "Tag2" }
            };
            _context.Tags.AddRange(tags);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllTags();
            var resultList = result.ToList(); // Materializa la consulta en una lista

            // Assert
            Assert.Equal(2, resultList.Count);
            Assert.Contains(resultList, t => t.Id == 1 && t.Name == "Tag1");
            Assert.Contains(resultList, t => t.Id == 2 && t.Name == "Tag2");
        }

        [Fact]
        public async Task GetTagsByIds_ShouldReturnCorrectTags()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Tag1" },
                new Tag { Id = 2, Name = "Tag2" },
                new Tag { Id = 3, Name = "Tag3" }
            };
            _context.Tags.AddRange(tags);
            await _context.SaveChangesAsync();

            var ids = new List<int> { 1, 3 };

            // Act
            var result = await _repository.GetTagsByIds(ids);
            var resultList = result.ToList(); // Materializa la consulta en una lista

            // Assert
            Assert.Equal(2, resultList.Count);
            Assert.Contains(resultList, t => t.Id == 1);
            Assert.Contains(resultList, t => t.Id == 3);
            Assert.DoesNotContain(resultList, t => t.Id == 2);
        }

        [Fact]
        public async Task GetTagById_ShouldReturnTag()
        {
            // Arrange
            var tag = new Tag { Id = 1, Name = "Tag1" };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTagById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Tag1", result.Name);
        }

        [Fact]
        public async Task ExistsTags_ShouldReturnExistingTagsIds()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { Id = 1 },
                new Tag { Id = 2 },
                new Tag { Id = 3 }
            };
            _context.Tags.AddRange(tags);
            await _context.SaveChangesAsync();

            var ids = new List<int> { 1, 3, 4 }; // 4 no existe

            // Act
            var result = await _repository.ExistsTags(ids);
            var resultList = result.ToList(); // Materializa la consulta en una lista

            // Assert
            Assert.Equal(2, resultList.Count);
            Assert.Contains(resultList, id => id == 1);
            Assert.Contains(resultList, id => id == 3);
            Assert.DoesNotContain(resultList, id => id == 4);
        }

        [Fact]
        public async Task AddTag_ShouldAddTag()
        {
            // Arrange
            var tag = new Tag { Id = 1, Name = "NewTag" };

            // Act
            await _repository.AddTag(tag);
            var result = await _context.Tags.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NewTag", result.Name);
        }

        [Fact]
        public async Task UpdateTag_ShouldUpdateTag()
        {
            // Arrange
            var tag = new Tag { Id = 1, Name = "OldName" };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            tag.Name = "UpdatedName";

            // Act
            await _repository.UpdateTag(tag);
            var result = await _context.Tags.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedName", result.Name);
        }

        [Fact]
        public async Task DeleteTag_ShouldDeleteTag()
        {
            // Arrange
            var tag = new Tag { Id = 1, Name = "TagToDelete" };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteTag(1);
            var result = await _context.Tags.FindAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}
