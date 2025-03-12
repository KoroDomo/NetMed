using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xunit;

public class NotificationRepositoryTests
{
    private readonly Mock<NetmedContext> _mockContext;
    private readonly Mock<ILogger<NotificationRepository>> _mockLogger;
    private readonly NotificationRepository _notificationRepository;
    private readonly JsonMessage _messagedMessage;

    public NotificationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<NetmedContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _mockContext = new Mock<NetmedContext>(options);
        _mockLogger = new Mock<ILogger<NotificationRepository>>();

    }


}