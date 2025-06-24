namespace FirstApi.Services;

using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs.DoctorSpecialities;


using FirstApi.Interfaces;
public class DoctorService : IDoctorService
{
    private readonly IRepository<int, Doctor> _doctorRepository;
    private readonly IRepository<int, Speciality> _specialityRepository;
    private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;

    public DoctorService(
        IRepository<int, Doctor> doctorRepository,
        IRepository<int, Speciality> specialityRepository,
        IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
    {
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
        _doctorSpecialityRepository = doctorSpecialityRepository;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _doctorRepository.GetAll();
    }
    public async Task<Doctor> GetDoctByName(string name)
    {
        var allDoctors = await _doctorRepository.GetAll();
        return allDoctors.FirstOrDefault(d => d.Name == name)
            ?? throw new Exception("Doctor not found");
    }
    public async Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)
    {
        var allSpecialities = await _specialityRepository.GetAll();
        var spec = allSpecialities.FirstOrDefault(s => s.Name == speciality);
        if (spec == null)
            throw new Exception("Speciality not found");

        var allDoctorSpecialities = await _doctorSpecialityRepository.GetAll();
        var doctorIds = allDoctorSpecialities
            .Where(ds => ds.SpecialityId == spec.Id)
            .Select(ds => ds.DoctorId)
            .ToList();

        var allDoctors = await _doctorRepository.GetAll();
        return allDoctors.Where(d => doctorIds.Contains(d.Id)).ToList();
    }
    public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctorDto)
    {
        var doctor = new Doctor
        {
            Name = doctorDto.Name,
            YearsOfExperience = doctorDto.YearsOfExperience,
            Status = "Active",
            DoctorSpecialities = new List<DoctorSpeciality>()
        };
        //Console.WriteLine($"Name: {doctor.Name}, YearsOfExperience: {doctor.YearsOfExperience}, Status: {doctor.Status}, DoctorSpecialities count: {doctor.DoctorSpecialities.Count}");

        var addedDoctor = await _doctorRepository.Add(doctor);
        //Console.WriteLine($"Name: {addedDoctor.Name}, YearsOfExperience: {addedDoctor.YearsOfExperience}, Status: {addedDoctor.Status}, DoctorSpecialities count: {addedDoctor.DoctorSpecialities.Count}");

        if (doctorDto.Specialities != null)
        {
            foreach (var specDto in doctorDto.Specialities)
            {
                var allSpecs = await _specialityRepository.GetAll();
                var speciality = allSpecs.FirstOrDefault(s => s.Name == specDto.Name);

                if (speciality == null)
                {
                    speciality = new Speciality { Name = specDto.Name, Status = "Active" };
                    speciality = await _specialityRepository.Add(speciality);
                }

                var doctorSpeciality = new DoctorSpeciality
                {
                    DoctorId = addedDoctor.Id,
                    SpecialityId = speciality.Id
                };
                await _doctorSpecialityRepository.Add(doctorSpeciality);
            }
        }

        return addedDoctor;
    }

}