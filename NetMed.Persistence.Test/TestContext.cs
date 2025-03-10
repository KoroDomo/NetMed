using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;

namespace NetMed.Persistence.Test
{
    public class TestContext : NetMedContext
    {
        private readonly DbContextOptions<NetMedContext> options = new DbContextOptionsBuilder<NetMedContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

        public TestContext(DbContextOptions<NetMedContext> options) : base(options)
        {

        }
    }

}
