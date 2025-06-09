namespace BookingSystem.Services;

using BookingSystem.Models;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class TicketService : ITicketService
{
    private readonly IRepository<int, Ticket> _ticketRepository;
    private readonly IRepository<string, Event> _eventRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TicketService(IRepository<int, Ticket> ticketRepository, IRepository<string, Event> eventRepository, IHttpContextAccessor httpContextAccessor)
    {
        _ticketRepository = ticketRepository;
        _eventRepository = eventRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Ticket> BookTicket(TicketDto ticketDto)
    {
        var curevent= await _eventRepository.Get(ticketDto.EventName);
        if (curevent == null)
        {
            throw new Exception("Event not found");
        }
        var eventid= curevent.Id;

        string? username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var newTicket = new Ticket
        {
            EventId = eventid,
            Quantity = ticketDto.Quantity,
            CustomerEmail = username ?? string.Empty,
            BookingDate = DateTime.UtcNow,
            Total= ticketDto.Quantity * curevent.Price,
            IsCancelled = false
        };
        return await _ticketRepository.Add(newTicket);
    }

    public async Task<Ticket> GetTicketById(int id)
    {
        return await _ticketRepository.Get(id);
    }

    public async Task<Ticket> CancelTicketById(int id)
    {
        var ticket = await _ticketRepository.Get(id);
        if (ticket == null)
        {
            throw new Exception("Ticket not found");
        }
        await _ticketRepository.Delete(id);
        return ticket;
    }
}