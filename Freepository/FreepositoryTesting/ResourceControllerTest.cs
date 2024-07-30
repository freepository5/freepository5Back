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
    public class ResourceControllerTests
    {
        private readonly Mock<IResourceRepository> _resourceRepositoryMock;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ResourceController _controller;

        public ResourceControllerTests()
        {
            _resourceRepositoryMock = new Mock<IResourceRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _mapperMock = new Mock<IMapper>();

            _controller = new ResourceController(
                _resourceRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAllResources_ShouldReturnOkResult_WithResourceDto()
        {
            // Arrange
            var resources = new List<Resource>
            {
                new Resource { Id = 1 }
            };
            var resourcesDto = new List<ResourceDTO>
            {
                new ResourceDTO { Id = 1 }
            };

            _resourceRepositoryMock.Setup(r => r.GetAllResources()).ReturnsAsync(resources);
            _mapperMock.Setup(m => m.Map<IEnumerable<ResourceDTO>>(resources)).Returns(resourcesDto);

            // Act
            var actionResult = await _controller.GetAllResources();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(resourcesDto, result.Value);
        }

        [Fact]
        public async Task AddResource_ShouldReturnCreatedAtAction_WhenResourceIsAdded()
        {
            // Arrange
            var createResourceDto = new CreateResourceDTO
            {
                TagIds = new List<int> { 1 },
                UserId = "1"
            };
            var tags = new List<Tag> { new Tag { Id = 1 } };
            var resource = new Resource { Id = 1, UserId = "1" };

            _tagRepositoryMock.Setup(t => t.GetTagsByIds(createResourceDto.TagIds)).ReturnsAsync(tags);
            _mapperMock.Setup(m => m.Map<Resource>(createResourceDto)).Returns(resource);
            _resourceRepositoryMock.Setup(r => r.AddResource(resource)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<ResourceDTO>(resource)).Returns(new ResourceDTO { Id = 1 });

            // Act
            var actionResult = await _controller.AddResource(createResourceDto);
            var result = actionResult.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal("GetAllResources", result.ActionName);
            Assert.Equal(1, ((ResourceDTO)result.Value).Id);
        }

        [Fact]
        public async Task AssignTag_ShouldReturnNoContent_WhenTagsAreAssigned()
        {
            // Arrange
            var id = 1;
            var request = new AssignTagRequest { TagIds = new List<int> { 1 } };
            var tagsExists = new List<int> { 1 };

            _resourceRepositoryMock.Setup(r => r.ExistResource(id)).ReturnsAsync(true);
            _tagRepositoryMock.Setup(t => t.ExistsTags(request.TagIds)).ReturnsAsync(tagsExists);
            _resourceRepositoryMock.Setup(r => r.AssignTag(id, tagsExists)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AssignTag(id, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateResource_ShouldReturnNoContent_WhenResourceIsUpdated()
        {
            // Arrange
            var id = 1;
            var updateResourceDto = new CreateResourceDTO
            {
                TagIds = new List<int> { 1 },
                UserId = "1"
            };
            var existingResource = new Resource { Id = 1 };
            var tags = new List<Tag> { new Tag { Id = 1 } };

            _resourceRepositoryMock.Setup(r => r.GetResourceById(id)).ReturnsAsync(existingResource);
            _tagRepositoryMock.Setup(t => t.GetTagsByIds(updateResourceDto.TagIds)).ReturnsAsync(tags);
            _mapperMock.Setup(m => m.Map(updateResourceDto, existingResource));
            _resourceRepositoryMock.Setup(r => r.UpdateResource(existingResource)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateResource(id, updateResourceDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteResource_ShouldReturnNoContent_WhenResourceIsDeleted()
        {
            // Arrange
            var id = 1;
            _resourceRepositoryMock.Setup(r => r.DeleteResource(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteResource(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
