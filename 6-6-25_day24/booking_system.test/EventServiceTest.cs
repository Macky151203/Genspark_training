using BookingSystem.Contexts;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Repositories;
using BookingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace BookingSystem.Tests;

public class EventServiceTest
{
    private BookingDbContext _context;
    private IHttpContextAccessor _httpContextAccessor;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BookingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BookingDbContext(options);

        // Seed with a sample category
        _context.Categories.Add(new Category { Name = "Music" });
        _context.SaveChanges();

        // Mock HttpContextAccessor with a test user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "testuser@example.com")
        }, "mock"));

        var httpContext = new DefaultHttpContext { User = user };
        var accessorMock = new Mock<IHttpContextAccessor>();
        accessorMock.Setup(a => a.HttpContext).Returns(httpContext);
        _httpContextAccessor = accessorMock.Object;
    }

    [Test]
    public async Task CreateEvent_WithNewCategory_CreatesCategoryAndEvent()
    {
        // Arrange
        var eventRepo = new EventRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var service = new EventService(eventRepo, categoryRepo, _httpContextAccessor);

        var eventDto = new EventDto
        {
            Title = "Test Concert",
            Description = "Live music",
            Date = DateTime.Now.AddDays(5),
            CategoryName = "Concert",
            Price = 100
        };

        // Act
        var result = await service.CreateEvent(eventDto);

        // Assert
        Assert.That(result.Title, Is.EqualTo("Test Concert"));
        Assert.That(result.CategoryId, Is.GreaterThan(0));
        Assert.That(result.CreatorEmail, Is.EqualTo("testuser@example.com"));
    }

    [Test]
    public async Task GetEventsByCategory_ReturnsCorrectEvents()
    {
        // Arrange
        var category = _context.Categories.First(c => c.Name == "Music");

        _context.Events.Add(new Event
        {
            Title = "Music Fest",
            Description = "Annual music festival",
            Date = DateTime.Now.AddDays(10),
            CategoryId = category.Id,
            CreatorEmail = "admin@site.com",
            Price = 250,
            IsCancelled = false
        });
        _context.SaveChanges();

        var service = new EventService(
            new EventRepository(_context),
            new CategoryRepository(_context),
            _httpContextAccessor);

        // Act
        var result = await service.GetEventsByCategoryAsync("Music");

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Title, Is.EqualTo("Music Fest"));
    }

    [Test]
    public async Task GetAllEvents_ReturnsAll()
    {
        // Arrange
        _context.Events.Add(new Event
        {
            Title = "Test Event",
            Description = "Generic",
            Date = DateTime.Now,
            CategoryId = _context.Categories.First().Id,
            CreatorEmail = "admin@site.com",
            Price = 100,
            IsCancelled = false
        });
        _context.SaveChanges();

        var service = new EventService(
            new EventRepository(_context),
            new CategoryRepository(_context),
            _httpContextAccessor);

        // Act
        var result = await service.GetAllEvents();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
