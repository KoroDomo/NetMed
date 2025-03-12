using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Validators;
using NPOI.SS.Formula.Functions;

namespace NetMed.Persistence.Test
{
    internal class NetworkTypeRepositoryUnitTest
    {
        private readonly NetMedContext _context;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly Mock<MessageMapper> _messageMapper;
        private readonly InsuranceProviderValidator _validator;
        private readonly InsuranceProviderRepository _repository;

        public NetworkTypeRepositoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new NetMedContext(options);

            _mockLogger = new Mock<ICustomLogger>();
            _messageMapper = new Mock<MessageMapper>();
            _validator = new InsuranceProviderValidator(_messageMapper.Object);
            _repository = new InsuranceProviderRepository(_context, _mockLogger.Object, _messageMapper.Object);
        }

    }
}
