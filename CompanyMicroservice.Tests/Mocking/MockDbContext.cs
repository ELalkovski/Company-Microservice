namespace CompanyMicroservice.Tests.Mocking
{
    using System;
    using CompanyMicroservice.Data;
    using Microsoft.EntityFrameworkCore;

    public static class MockDbContext
    {
        public static CompanyMsContext GetContext()
        {
            var options = new DbContextOptionsBuilder<CompanyMsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CompanyMsContext(options);
        }
    }
}
