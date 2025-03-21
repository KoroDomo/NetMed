using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Loggin.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;

namespace NetMed.Persistence.Test
{
    public class NotificationRepositoryTests
    {
        private readonly NetmedContext _context;
        private readonly ILoggerCustom _Logger;
        private readonly NotificationRepository _notificationRepository;
        private readonly JsonMessage _messagedMessage;
        

        public NotificationRepositoryTests()
        {
            var opinios = new DbContextOptionsBuilder<NetmedContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options;

            _context = new NetmedContext(opinios);

            _messagedMessage = new JsonMessage();

            var FactoryLogger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
            });

            _Logger = new LoggerCustom(FactoryLogger.CreateLogger<LoggerCustom>());

            _notificationRepository = new NotificationRepository(_context, _Logger, _messagedMessage);

        }

        [Fact]
        public async Task CreateNotificationAsync_WhernNotificationIsValid_ReturnSuccess()
        {
            var notification = new Notification { Id = 1, UserID = 1, Message = "I AM MUSIC" };

            var result = await _notificationRepository.CreateNotificationAsync(notification);

            Assert.True(result.Success);
            Assert.Equal(_messagedMessage.SuccessMessages["NotificationCreated"], result.Message);

        }

        [Fact]
        public async Task CreateNotificationAsync_WhenNotificationIsNotValid_ReturnFAiled()
        {
            var notification = new Notification { Id = -1, UserID = 1, Message = "I AM MUSIC" };

            var result = await _notificationRepository.CreateNotificationAsync(notification);

            Assert.False(result.Success);
            Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);

        }


        [Fact]
        public async Task GetNotificationsByUserIdAsync_WhernUserIdIsValid_GetNotificationsByUserId()
        {
            var result = await _notificationRepository.GetNotificationsByUserIdAsync(2);
            Assert.True(result.Success);
            Assert.Equal(_messagedMessage.SuccessMessages["NotificationFound"], result.Message);
        }

        [Fact]
        public async Task GetNotificationsByUserIdAsync_WhernReturnFailure_GetNotificationsByUserId()
        {
            var result = await _notificationRepository.GetNotificationsByUserIdAsync(-2);
            Assert.False(result.Success);
            Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
        }

        [Fact]
        public async Task GetNotificationByIdAsync_WhenNotificationIdIsNotValid_ReturnsNotificationFailed()
        {

            var notification = new Notification { Id = -9, UserID = 1, Message = "I AM MUSIC" };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            var result = await _notificationRepository.GetNotificationByIdAsync(notification.Id);

            Assert.False(result.Success);
            Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
        }

        [Fact]
        public async Task GetNotificationByIdAsync_WhenNotificationIdIsValid_ReturnsNotification()
        {

            var notification = new Notification { Id = 9, UserID = 1, Message = "I AM MUSIC" };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            var result = await _notificationRepository.GetNotificationByIdAsync(notification.Id);

            Assert.True(result.Success);
            Assert.Equal(_messagedMessage.SuccessMessages["NotificationFound"], result.Message);
            Assert.NotNull(result.Data);
        }


        [Fact]
        public async Task UpdateNotificationAsync_WhenNotificationIsValid_ReturnsSuccess()
        {
            var notification = new Notification { Id = 43, UserID = 1, Message = "HURRY UP TOMORROW" };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notification.Message = _messagedMessage.SuccessMessages["NotificationUpdated"];
            var result = await _notificationRepository.UpdateNotificationAsync(notification);

            Assert.True(result.Success);
            Assert.Equal(_messagedMessage.SuccessMessages["NotificationUpdated"], result.Message);
            Assert.NotNull(result.Data);
        }
       

        [Fact]
        public async Task UpdateNotificationAsync_WhenNotificationIsNotValid_ReturnsFailed()
        {
            var notification = new Notification { Id = 43, UserID = -1, Message = "HURRY UP TOMORROW" };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notification.Message = _messagedMessage.SuccessMessages["NotificationUpdated"];
            var result = await _notificationRepository.UpdateNotificationAsync(notification);

            Assert.False(result.Success);
            Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
        }

        [Fact]
        public async Task DeleteNotificationAsync_WhenNotificationIsValid_ReturnsSuccess()
        {
            var notification = new Notification { Id = 43, UserID = 1, Message = "HURRY UP TOMORROW" };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notification.Message = _messagedMessage.SuccessMessages["NotificationDeleted"];
            var result = await _notificationRepository.DeleteNotificationAsync(notification.Id);

            Assert.True(result.Success);
            Assert.Equal(_messagedMessage.SuccessMessages["NotificationDeleted"], result.Message);
        }



        [Fact]
        public async Task DeleteNotificationAsync_WhenNotificationIsNotValid_ReturnsFailed()
        {
            var notification = new Notification { Id = -43, UserID = 1, Message = "HURRY UP TOMORROW" };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notification.Message = _messagedMessage.SuccessMessages["NotificationDeleted"];
            var result = await _notificationRepository.DeleteNotificationAsync(notification.Id);

            Assert.False(result.Success);
            Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
        }

    }

}