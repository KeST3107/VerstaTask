namespace VerstaTask.Tests.Features
{
    using Microsoft.EntityFrameworkCore;
    using VerstaTask.EF;
    using VerstaTask.Tests.Controllers;


    public class InMemoryItemsTest : OrderControllerTest
    {
        public InMemoryItemsTest()
            : base(
                new DbContextOptionsBuilder<VerstaContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}
