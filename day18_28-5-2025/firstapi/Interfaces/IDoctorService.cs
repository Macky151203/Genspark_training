namespace FirstApi.Interfaces;

using FirstApi.Models;
using FirstApi.Models.DTOs.DoctorSpecialities;

public interface IDoctorService
{
    public Task<Doctor> GetDoctByName(string name);
    public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality);
    public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);

    public Task<IEnumerable<Doctor>> GetAllDoctors();
}
