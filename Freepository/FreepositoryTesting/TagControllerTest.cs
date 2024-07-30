using AutoMapper;
using Freepository.Controllers;
using Freepository.DTO_s;
using Freepository.Models;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FreepositoryTesting
{
    public class TagControllerTest
    {
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TagController _controller;

        public TagControllerTest()
        {
            _tagRepositoryMock = new Mock<ITagRepository>();
            _mapperMock = new Mock<IMapper>();

            _controller = new TagController(
                _tagRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAllTags_ShouldReturnOkResult_WithTagDto()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { Id = 1 }
            };
            var tagsDto = new List<TagDTO>
            {
                new TagDTO { Id = 1 }
            };

            _tagRepositoryMock.Setup(r => r.GetAllTags()).ReturnsAsync(tags);
            _mapperMock.Setup(m => m.Map<IEnumerable<TagDTO>>(tags)).Returns(tagsDto);

            // Act
            var actionResult = await _controller.GetAllTags();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(tagsDto, result.Value);
        }

        [Fact]
        public async Task AddTag_ShouldReturnCreatedAtAction_WhenTagIsAdded()
        {
            // Arrange
            var createTagDto = new CreateTagDTO
            {
                Name = "TestTag"
            };
            var tag = new Tag { Id = 1, Name = "TestTag" };

            _mapperMock.Setup(m => m.Map<Tag>(createTagDto)).Returns(tag);
            _tagRepositoryMock.Setup(r => r.AddTag(tag)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<TagDTO>(tag)).Returns(new TagDTO { Id = 1, Name = "TestTag" });

            // Act
            var actionResult = await _controller.AddTag(createTagDto);
            var result = actionResult.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal("GetAllTags", result.ActionName);
            Assert.Equal(1, ((TagDTO)result.Value).Id);
        }

        [Fact]
        public async Task UpdateTag_ShouldReturnNoContent_WhenTagIsUpdated()
        {
            // Arrange
            var id = 1;
            var updateTagDto = new CreateTagDTO
            {
                Name = "UpdatedTag"
            };
            var existingTag = new Tag { Id = 1, Name = "OldTag" };

            _tagRepositoryMock.Setup(r => r.GetTagById(id)).ReturnsAsync(existingTag);
            _mapperMock.Setup(m => m.Map(updateTagDto, existingTag));
            _tagRepositoryMock.Setup(r => r.UpdateTag(existingTag)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateTag(id, updateTagDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTag_ShouldReturnNoContent_WhenTagIsDeleted()
        {
            // Arrange
            var id = 1;
            _tagRepositoryMock.Setup(r => r.DeleteTag(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTag(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
