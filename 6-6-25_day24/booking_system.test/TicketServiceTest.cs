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

public class TicketServiceTest
{
    private BookingDbContext _context;
    private IHttpContextAccessor _httpContextAccessor;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BookingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new BookingDbContext(options);

        // Seed event
        _context.Events.Add(new Event
        {
            Title = "MusicFest",
            Description = "Annual music event",
            Date = DateTime.UtcNow.AddDays(10),
            Price = 200,
            CreatorEmail = "admin@site.com",
            CategoryId = 1,
            IsCancelled = false
        });
        _context.SaveChanges();

        // Mock user in HttpContext
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "customer@example.com")
        }, "mock"));

        var context = new DefaultHttpContext { User = user };
        var accessorMock = new Mock<IHttpContextAccessor>();
        accessorMock.Setup(a => a.HttpContext).Returns(context);
        _httpContextAccessor = accessorMock.Object;
    }

    [Test]
    public async Task BookTicket_ValidData_ReturnsTicket()
    {
        // Arrange
        var ticketRepo = new TicketRepository(_context);
        var eventRepo = new EventRepository(_context);
        var service = new TicketService(ticketRepo, eventRepo, _httpContextAccessor);

        var dto = new TicketDto
        {
            EventName = "MusicFest",
            Quantity = 2
        };

        // Act
        var result = await service.BookTicket(dto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Quantity, Is.EqualTo(2));
        Assert.That(result.CustomerEmail, Is.EqualTo("customer@example.com"));
        Assert.That(result.Total, Is.EqualTo(400));
    }

    [Test]
    public async Task GetTicketById_ExistingId_ReturnsTicket()
    {
        // Arrange
        var ticketRepo = new TicketRepository(_context);
        var eventRepo = new EventRepository(_context);

        var ticket = new Ticket
        {
            EventId = _context.Events.First().Id,
            Quantity = 1,
            CustomerEmail = "customer@example.com",
            BookingDate = DateTime.UtcNow,
            Total = 200,
            IsCancelled = false
        };

        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        var service = new TicketService(ticketRepo, eventRepo, _httpContextAccessor);

        // Act
        var result = await service.GetTicketById(ticket.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Total, Is.EqualTo(200));
    }

    [Test]
    public async Task CancelTicketById_ValidId_DeletesTicket()
    {
        // Arrange
        var ticketRepo = new TicketRepository(_context);
        var eventRepo = new EventRepository(_context);

        var ticket = new Ticket
        {
            EventId = _context.Events.First().Id,
            Quantity = 1,
            CustomerEmail = "customer@example.com",
            BookingDate = DateTime.UtcNow,
            Total = 200,
            IsCancelled = false
        };
        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        var service = new TicketService(ticketRepo, eventRepo, _httpContextAccessor);

        // Act
        var cancelled = await service.CancelTicketById(ticket.Id);
        var shouldBeNull = await service.GetTicketById(ticket.Id);

        // Assert
        Assert.That(cancelled.Id, Is.EqualTo(ticket.Id));
        Assert.That(shouldBeNull, Is.Null);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
