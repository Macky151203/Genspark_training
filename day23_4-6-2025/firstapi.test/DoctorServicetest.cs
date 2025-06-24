using FirstApi.Contexts;
using FirstApi.Repositories;
using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Services;
using FirstApi.Models.DTOs;
using FirstApi.Models.DTOs.DoctorSpecialities;
using FirstApi.Misc;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

namespace firstapi.test;

public class DoctorServiceTest
{
    private ClinicContext _context;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ClinicContext>()
                            .UseInMemoryDatabase("TestDb")
                            .Options;
        _context = new ClinicContext(options);

    }
    [TestCase("General")]
    public async Task TestGetDoctorBySpeciality(string speciality)
    {
        Mock<DoctorRepository> doctorRepositoryMock = new Mock<DoctorRepository>(_context);
        Mock<SpecialityRepository> specialityRepositoryMock = new(_context);
        Mock<DoctorSpecialityRepository> doctorSpecialityRepositoryMock = new(_context);
        Mock<UserRepository> userRepositoryMock = new(_context);
        Mock<OtherFuncinalitiesImplementation> otherContextFunctionitiesMock = new(_context);
        Mock<EncryptionService> encryptionServiceMock = new();
        Mock<IMapper> mapperMock = new();

        otherContextFunctionitiesMock.Setup(ocf => ocf.GetDoctorsBySpeciality(It.IsAny<string>()))
                                    .ReturnsAsync((string specilaity) => new List<DoctorsBySpecialityResponseDto>{
                                   new DoctorsBySpecialityResponseDto
                                        {
                                            Dname = "test",
                                            Yoe = 2,
                                            Id=1
                                        },
                                        new DoctorsBySpecialityResponseDto
                                        {
                                            Dname = "test2",
                                            Yoe = 1,
                                            Id=2,
                                        }
                            });
        IDoctorService doctorService = new DoctorService(
    doctorRepositoryMock.Object,
    specialityRepositoryMock.Object,
    doctorSpecialityRepositoryMock.Object,
    new Mock<IRepository<string, Appointment>>().Object,
    otherContextFunctionitiesMock.Object,
    userRepositoryMock.Object,
    encryptionServiceMock.Object,
    mapperMock.Object
);


        //Assert.That(doctorService, Is.Not.Null);
        //Action
        var result = await doctorService.GetDoctorsBySpeciality(speciality);
        Console.WriteLine(result.Count());
        //Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [TestCase("Kane")]
    public async Task GetDoctByName(string name)
    {
        // Arrange
        Mock<DoctorRepository> doctorRepositoryMock = new Mock<DoctorRepository>(_context);
        Mock<SpecialityRepository> specialityRepositoryMock = new(_context);
        Mock<DoctorSpecialityRepository> doctorSpecialityRepositoryMock = new(_context);
        Mock<UserRepository> userRepositoryMock = new(_context);
        Mock<IOtherContextFunctionities> otherContextFunctionitiesMock = new();
        Mock<EncryptionService> encryptionServiceMock = new();
        Mock<IMapper> mapperMock = new();

        otherContextFunctionitiesMock.Setup(ocf => ocf.GetDoctorsByName(It.IsAny<string>()))
            .ReturnsAsync((string n) => new List<DoctorsByNameResponseDto>
            {
            new DoctorsByNameResponseDto { Dname = "test", Id = 1 }
            });

        IDoctorService doctorService = new DoctorService(
            doctorRepositoryMock.Object,
            specialityRepositoryMock.Object,
            doctorSpecialityRepositoryMock.Object,
            new Mock<IRepository<string, Appointment>>().Object,
            otherContextFunctionitiesMock.Object,
            userRepositoryMock.Object,
            encryptionServiceMock.Object,
            mapperMock.Object
        );

        // Act
        var result = await doctorService.GetDoctByName(name);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
    }


    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }


}