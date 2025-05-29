namespace FirstApi.Services;

using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs.DoctorSpecialities;
using FirstApi.Misc;


using FirstApi.Interfaces;
public class DoctorService : IDoctorService
{
    DoctorMapper _doctorMapper;
    SpecialityMapper _specialityMapper;
    private readonly IRepository<int, Doctor> _doctorRepository;
    private readonly IRepository<int, Speciality> _specialityRepository;
    private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;

    private readonly IOtherContextFunctionities _otherContextFunctionities;

    public DoctorService(
        IRepository<int, Doctor> doctorRepository,
        IRepository<int, Speciality> specialityRepository,
        IRepository<int, DoctorSpeciality> doctorSpecialityRepository,
        IOtherContextFunctionities otherContextFunctionities
        )
    {
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
        _doctorSpecialityRepository = doctorSpecialityRepository;
        _doctorMapper = new DoctorMapper();
        _specialityMapper = new SpecialityMapper();
        _otherContextFunctionities = otherContextFunctionities;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _doctorRepository.GetAll();
    }
    public async Task<ICollection<DoctorsByNameResponseDto>> GetDoctByName(string name)
    {
        // var allDoctors = await _doctorRepository.GetAll();
        // return allDoctors.FirstOrDefault(d => d.Name == name)
        //     ?? throw new Exception("Doctor not found");
        var result = await _otherContextFunctionities.GetDoctorsByName(name);
        
        return result;
    }
    
    public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctorDto)
    {
        var doctor = _doctorMapper.MapDoctorAddRequestDto(doctorDto);
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
                    speciality = _specialityMapper.MapSpecialityAddRequestDto(specDto);
                    speciality = await _specialityRepository.Add(speciality);
                }

                var doctorSpeciality = _specialityMapper.MapDoctorSpeciality(addedDoctor.Id, speciality.Id);
                await _doctorSpecialityRepository.Add(doctorSpeciality);
            }
        }

        return addedDoctor;
    }
    public async Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality)
    {
        var result = await _otherContextFunctionities.GetDoctorsBySpeciality(speciality);
        return result;
    }


}