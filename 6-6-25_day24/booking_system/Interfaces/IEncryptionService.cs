namespace BookingSystem.Interfaces;
using BookingSystem.Models;
public interface IEncryptionService
{
    public Task<EncryptModel> EncryptData(EncryptModel data);
}