using ApplicationCore.Entities.IdentityAggregate;
using ApplicationCore.Interfaces;
using NSubstitute;
using ApplicationCore.Services;
using NSubstitute.ExceptionExtensions;

namespace ZUniTest.ApplicationCore.Services
{
    public class IdentityServiceTest
    {
        // mock repository
        private readonly IRepository<IdentityInfos> _mockIdentityInfoRepo = Substitute.For<IRepository<IdentityInfos>>();
        #region SignUp
        [Fact]
        public async Task SignUp_CallsAddAsyncOnce()
        {
            // Arrange
            string userName = "testUser";
            string email = "test@example.com";
            string passWord = "password123";

            var identityService = new IdentityService(_mockIdentityInfoRepo);

            // Act
            var result = await identityService.SignUp(userName, email, passWord);

            // Assert
            Assert.True(result);
            await _mockIdentityInfoRepo.Received(1).AddAsync(Arg.Any<IdentityInfos>()); // 確保 AddAsync 方法被正確調用一次
        }


        [Fact]
        public async Task SignUp_CallsAddAsyncOnce_Exception()
        {
            // Arrange
            string userName = "testUser";
            string email = "test@example.com";
            string passWord = "password123";

            // mock : Simulate db's actions
            _mockIdentityInfoRepo.AddAsync(Arg.Any<IdentityInfos>()).Throws(new Exception("Database error"));

            var identityService = new IdentityService(_mockIdentityInfoRepo);

            // Act
            var result = await identityService.SignUp(userName, email, passWord);

            // Assert
            Assert.False(result);
        }
        #endregion 
    }
}