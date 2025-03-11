using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Domain.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Test;
public class UnitTestUsers
{
    private readonly Mock<IUsersRepository> _mockUsersRepository;
    private readonly NetMedContext _context;

    public UnitTestUsers()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _context = new NetMedContext(options);
        _mockUsersRepository = new Mock<IUsersRepository>();
    }

    [Fact]
    public async Task GetActiveUsersAsyncReturnsActiveUsers()
    {
        //arrange
        var mockUsersActive = new Mock<IUsersRepository>();
        mockUsersActive.Setup(x => x.GetActiveUsersAsync(true)).ReturnsAsync(new OperationResult { Success = true });
        //act
        var result = await mockUsersActive.Object.GetActiveUsersAsync(true);
        //assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetEmailAsyncReturnsEmail()
    {
      
        var mockUsersEmail = new Mock<IUsersRepository>();
        mockUsersEmail.Setup(x => x.GetEmailAsync("Correo de prueba")).ReturnsAsync(new OperationResult { Success = true });
      
        var result = await mockUsersEmail.Object.GetEmailAsync("Correo de prueba");
    
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetByRoleByIDAsyncReturnsRole()
    {
       
        var mockUsersRole = new Mock<IUsersRepository>();
        mockUsersRole.Setup(x => x.GetByRoleByIDAsync(1)).ReturnsAsync(new OperationResult { Success = true });
  
        var result = await mockUsersRole.Object.GetByRoleByIDAsync(1);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task SearchByNameAsyncReturnsName()
    {
       
        var mockUsersName = new Mock<IUsersRepository>();
        mockUsersName.Setup(x => x.SearchByNameAsync("Nombre de prueba")).ReturnsAsync(new OperationResult { Success = true });
    
        var result = await mockUsersName.Object.SearchByNameAsync("Nombre de prueba");
    
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetUsersRegisteredInRangeAsyncReturnsRange()
    {
  
        var mockUsersRange = new Mock<IUsersRepository>();
        mockUsersRange.Setup(x => x.GetUsersRegisteredInRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                      .ReturnsAsync(new OperationResult { Success = true });
   
        var result = await mockUsersRange.Object.GetUsersRegisteredInRangeAsync(DateTime.Now, DateTime.Now);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetPhoneNumberAsyncReturnsPhoneNumber()
    {

        var mockUsersPhone = new Mock<IUsersRepository>();
        mockUsersPhone.Setup(x => x.GetPhoneNumberAsync("1234567890")).ReturnsAsync(new OperationResult { Success = true });
 
        var result = await mockUsersPhone.Object.GetPhoneNumberAsync("1234567890");
      
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetAddressAsyncReturnsAddress()
    {
     
        var mockUsersAddress = new Mock<IUsersRepository>();
        mockUsersAddress.Setup(x => x.GetAddressAsync("Direccion de prueba")).ReturnsAsync(new OperationResult { Success = true });

        var result = await mockUsersAddress.Object.GetAddressAsync("Direccion de prueba");
       
        Assert.True(result.Success);
    }

}


