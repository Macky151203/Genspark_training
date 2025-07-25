﻿using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using BookingSystem.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Tests
{
    public class AdminServiceTests
    {
        private BookingDbContext _context;
        private AdminService _adminService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBookingDb")
                .Options;

            _context = new BookingDbContext(options);

            var adminRepo = new AdminRepository(_context);
            var userRepo = new UserRepository(_context);

            var mockEncryptionService = new Mock<IEncryptionService>();
            mockEncryptionService.Setup(e => e.EncryptData(It.IsAny<EncryptModel>()))
                .ReturnsAsync((EncryptModel model) =>
                {
                    return new EncryptModel
                    {
                        Data = model.Data,
                        EncryptedData = "encrypted_" + model.Data
                    };
                });

            _adminService = new AdminService(adminRepo, userRepo, mockEncryptionService.Object);
        }

        [Test]
        public async Task RegisterAdmin()
        {
            // Arrange
            var adminDto = new AdminDto
            {
                Name = "Alice",
                Email = "alice@example.com",
                Password = "password123"
            };

            // Act
            var result = await _adminService.RegisterAdmin(adminDto);

            // Assert
            Assert.That(result, Is.Not.Null);

           
            var admins = _context.Admins.ToList();

            Assert.That(admins.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAdminByEmail()
        {
            // Arrange
            var admin = new Admin { Email = "john@example.com", Name = "John" };
            _context.Admins.Add(admin);
            _context.SaveChanges();

            // Act
            var result = await _adminService.GetAdminByName("John");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John"));
        }

        [Test]
        public async Task GetAllAdmins()
        {
            // Arrange
            _context.Admins.AddRange(new List<Admin>
            {
                new Admin { Name = "Admin1", Email = "a1@example.com" },
                new Admin { Name = "Admin2", Email = "a2@example.com" }
            });
            _context.SaveChanges();

            // Act
            var result = await _adminService.GetAllAdmins();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
