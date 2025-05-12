using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmApi.Controllers;
using FilmApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FilmApi.Test
{
    public class FilmItemsControllerTests
    {
        private FilmContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<FilmContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new FilmContext(options);

            return context;
        }

        [Fact]
        public async Task GetFilmItems_ReturnsAllItems()
        {
            // Arrange
            var context = GetTestDbContext();
            var testFilm = new FilmItem
            {
                Id = 1,
                Manufacturer = "Kodak",
                Name = "Test Film",
                Format = FilmFormat.Format35mm,
                Type = FilmType.BlackAndWhite,
                Quantity = 5
            };
            context.FilmItems.Add(testFilm);
            await context.SaveChangesAsync();

            var controller = new FilmItemsController(context);

            // Act
            var result = await controller.GetFilmItems();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<FilmItem>>>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<FilmItem>>(actionResult.Value);
            Assert.Single(items);
        }

        [Fact]
        public async Task GetFilmItem_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var context = GetTestDbContext();
            var controller = new FilmItemsController(context);

            // Act
            var result = await controller.GetFilmItem(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostFilmItem_CreatesNewItem()
        {
            // Arrange
            var context = GetTestDbContext();
            var controller = new FilmItemsController(context);
            var newFilm = new FilmItem
            {
                Manufacturer = "Kodak",
                Name = "Test Film",
                Format = FilmFormat.Format35mm,
                Type = FilmType.BlackAndWhite,
                Quantity = 5
            };

            // Act
            var result = await controller.PostFilmItem(newFilm);

            // Assert
            var actionResult = Assert.IsType<ActionResult<FilmItem>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<FilmItem>(createdAtActionResult.Value);
            Assert.Equal(newFilm.Name, returnValue.Name);
            Assert.Equal(1, await context.FilmItems.CountAsync());
        }

        [Fact]
        public async Task DeleteFilmItem_RemovesItem_WhenItemExists()
        {
            // Arrange
            var context = GetTestDbContext();
            var testFilm = new FilmItem
            {
                Id = 1,
                Manufacturer = "Kodak",
                Name = "Test Film",
                Format = FilmFormat.Format35mm,
                Type = FilmType.BlackAndWhite,
                Quantity = 5
            };
            context.FilmItems.Add(testFilm);
            await context.SaveChangesAsync();

            var controller = new FilmItemsController(context);

            // Act
            var result = await controller.DeleteFilmItem(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(0, await context.FilmItems.CountAsync());
        }

        [Fact]
        public async Task PutFilmItem_UpdatesItem_WhenItemExists()
        {
            // Arrange
            var context = GetTestDbContext();
            var testFilm = new FilmItem
            {
                Id = 1,
                Manufacturer = "Kodak",
                Name = "Test Film",
                Format = FilmFormat.Format35mm,
                Type = FilmType.BlackAndWhite,
                Quantity = 5
            };
            context.FilmItems.Add(testFilm);
            await context.SaveChangesAsync();

            var controller = new FilmItemsController(context);
            testFilm.Name = "Updated Name";

            // Act
            var result = await controller.PutFilmItem(1, testFilm);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var updatedFilm = await context.FilmItems.FindAsync(1);
            Assert.NotNull(updatedFilm);
            Assert.Equal("Updated Name", updatedFilm.Name);
        }
    }
}