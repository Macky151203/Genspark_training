using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Services;
using BookingSystem.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookingSystem.Tests
{
    public class CustomerServiceTests
    {
        private BookingDbContext _context;
        private CustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCustomerDb")
                .Options;

            _context = new BookingDbContext(options);

            var customerRepo = new CustomerRepository(_context);
            var userRepo = new UserRepository(_context);

            var encryptionServiceMock = new Mock<IEncryptionService>();
            encryptionServiceMock.Setup(e => e.EncryptData(It.IsAny<EncryptModel>()))
                .ReturnsAsync((EncryptModel model) => new EncryptModel
                {
                    Data = model.Data,
                    EncryptedData = "enc_" + model.Data
                });

            _customerService = new CustomerService(customerRepo, userRepo, encryptionServiceMock.Object);
        }

        [Test]
        public async Task RegisterCustomer_Should_Add_User_And_Customer()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                Name = "Bob",
                Email = "bob@example.com",
                Password = "securepass"
            };

            // Act
            var result = await _customerService.RegisterCustomer(customerDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Email, Is.EqualTo("bob@example.com"));

            var users = _context.Users.ToList();
            var customers = _context.Customers.ToList();

            Assert.That(users.Count, Is.EqualTo(1));
            Assert.That(users[0].Role, Is.EqualTo("Customer"));
            Assert.That(users[0].Password, Is.EqualTo("enc_securepass"));

            Assert.That(customers.Count, Is.EqualTo(1));
        }

        [TestCase("Alice")]
        public async Task GetCustomerByEmail_Should_Return_Correct_Customer(string name)
        {
            // Arrange
            var customer = new Customer
            {
                Name = "Alice",
                Email = "alice@example.com"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Act
            var result = await _customerService.GetCustomerByEmail(name);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Alice"));
        }

        [Test]
        public async Task GetAllCustomers_Should_Return_All_Customers()
        {
            // Arrange
            _context.Customers.AddRange(new List<Customer>
            {
                new Customer { Name = "C1", Email = "c1@example.com" },
                new Customer { Name = "C2", Email = "c2@example.com" }
            });
            _context.SaveChanges();

            // Act
            var result = await _customerService.GetAllCustomers();

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
