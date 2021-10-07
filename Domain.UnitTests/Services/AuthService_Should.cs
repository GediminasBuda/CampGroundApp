using AutoFixture;
using AutoFixture.Xunit2;
using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Domain.Clients.Firebase.Models;
using Domain.Clients.Firebase.Models.ResponseModels;
using Domain.Services;
using Domain.UnitTests.Attributes;
using FluentAssertions;
using Moq;
using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Services
{
    public class AuthService_Should
    {
       /* private readonly Mock<IFirebaseClient> _firebaseClientMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthService _sut;

        public AuthService_Should()
        {
            _firebaseClientMock = new Mock<IFirebaseClient>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new AuthService(_firebaseClientMock.Object, _userRepositoryMock.Object);
        }*/

        // SignIn_WithSignInRequest_ReturnsSignInResponse
        [Theory]
        [AutoMoq]
        public async Task SignInAsync_WithSignInRequest_ReturnsSignInResponse(
            SignInRequest signInRequest,
            FirebaseSignInResponse firebaseSignInResponse,
            UserReadModel userReadModel,
            [Frozen] Mock<IFirebaseClient> firebaseClientMock,
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            AuthService sut)
        {
            // Arrange
            firebaseSignInResponse.Email = signInRequest.Email;
            userReadModel.FirebaseId = firebaseSignInResponse.FirebaseId;
            userReadModel.Email = firebaseSignInResponse.Email;


           /* var signInRequest = new SignInRequest
            {
                Email = "Rokas",
                Password = "Passwordas"
            };

            var firebaseSignInResponse = new FirebaseSignInResponse
            {
                IdToken = Guid.NewGuid().ToString(),
                Email = signInRequest.Email,
                FirebaseId = Guid.NewGuid().ToString()
            };

            var userReadModel = new UserReadModel
            {
                UserId = Guid.NewGuid(),
                FirebaseId = firebaseSignInResponse.FirebaseId,
                Email = firebaseSignInResponse.Email,
                DateCreated = DateTime.Now
            };*/

            firebaseClientMock
                .Setup(firebaseClient => firebaseClient.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(firebaseSignInResponse);

            userRepositoryMock
                .Setup(userRepository => userRepository.GetByIdAsync(firebaseSignInResponse.FirebaseId))
                .ReturnsAsync(userReadModel);
  
            // Act
            var result = await sut.SignInAsync(signInRequest);

            // Assert
            signInRequest.Email.Should().BeEquivalentTo(result.Email);
            firebaseSignInResponse.IdToken.Should().BeEquivalentTo(result.IdToken);
        }

        [Theory]
        [AutoMoq]
        public async Task SignUpAsync_With_SignUpRequest(
            SignUpRequest signUpRequest, 
            FirebaseSignUpResponse firebaseSignUpResponse,
            [Frozen] Mock<IFirebaseClient> firebaseClientMock,
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            AuthService sut) 
        {
            //Arrange
            /*var fixture = new Fixture();
            var signUpRequest = fixture.Create<SignUpRequest>();// NuGet AutoFixture helps to create object (the same as new SignUprequest bellow);
            var firebaseSignUpResponse = fixture.Create<FirebaseSignUpResponse>();*/
            /*var signUpRequest = new SignUpRequest
            {
                Email = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };*/

            /*var firebaseSignUpResponse = new FirebaseSignUpResponse
            {
                IdToken = Guid.NewGuid().ToString(),
                Email = signUpRequest.Email,
                FirebaseId = Guid.NewGuid().ToString()
            };*/
            firebaseSignUpResponse.Email = signUpRequest.Email;

            firebaseClientMock
                .Setup(firebaseClient => firebaseClient.SignUpAsync(signUpRequest.Email, signUpRequest.Password))
                .ReturnsAsync(firebaseSignUpResponse);

            // Act

            var result = await sut.SignUpAsync(signUpRequest);

            //Assert

            userRepositoryMock
                .Verify(userRepository => userRepository.SaveAsync(It.Is<UserWriteModel>(model =>
                model.FirebaseId.Equals(firebaseSignUpResponse.FirebaseId) &&
                model.Email.Equals(firebaseSignUpResponse.Email))), Times.Once);

            firebaseClientMock
                .Verify(firebaseClient => firebaseClient.SignUpAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            Assert.IsType<Guid>(result.UserId);
           /* Assert.Equal(firebaseSignUpResponse.IdToken, result.IdToken);
            Assert.Equal(firebaseSignUpResponse.Email, result.Email);*/
            Assert.IsType<DateTime>(result.DateCreated);

            result.IdToken.Should().BeEquivalentTo(firebaseSignUpResponse.IdToken);// another method to chaeck values with NuGet Package FluentAssertions;
            result.Email.Should().BeEquivalentTo(firebaseSignUpResponse.Email);
        }
    }
}

