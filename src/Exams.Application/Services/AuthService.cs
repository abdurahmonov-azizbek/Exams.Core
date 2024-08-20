using Exams.Application.Interfaces;
using Exams.Domain.Dtos;
using Exams.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Exams.Application.Services
{
    public class AuthService(
        IPasswordHasherService passwordHasherService,
        IUserService userService,
        IVerificationCodeService verificationCodeService,
        ISmsSenderService smsSenderService,
        ITokenGeneratorService tokenGeneratorService) : IAuthService
    {
        public async ValueTask<string> LoginAsync(LoginDetails loginDetails)
        {
            var user = await userService.GetAll().FirstOrDefaultAsync(user
                => user.PhoneNumber == loginDetails.PhoneNumber);

            if (user is null)
            {
                throw new InvalidOperationException("User not found with this phone number");
            }

            if (!passwordHasherService.Verify(loginDetails.Password, user.Password))
            {
                throw new InvalidOperationException("Phone number or password incorrect");
            }

            var token = tokenGeneratorService.GetToken(user);

            return token;
        }

        public async ValueTask<bool> RegisterAsync(RegisterDetails registerDetails)
        {
            if (registerDetails.Password.Length is < 8 or > 32)
            {
                return false;
            }

            var userDto = new UserDto
            {
                FirstName = registerDetails.FirstName,
                LastName = registerDetails.LastName,
                PhoneNumber = registerDetails.PhoneNumber,
                Password = passwordHasherService.Hash(registerDetails.Password),
                Role = Domain.Enums.Role.User,
                IsVerified = false
            };

            var createUserTaskResult = await userService.CreateAsync(userDto);

            if (!createUserTaskResult)
                return createUserTaskResult;

            var createdUser = await userService.GetAll().FirstOrDefaultAsync(user =>
                user.PhoneNumber == userDto.PhoneNumber);

            if (createdUser is null)
            {
                return false;
            }

            var random = new Random();

            var verificationCode = new VerificationCodeDto
            {
                Code = random.Next(10_000, 99_999).ToString(),
                UserId = createdUser.Id
            };

            await verificationCodeService.CreateAsync(verificationCode);
            await smsSenderService.SendAsync(
                new SendSmsRequest
                {
                    Message = $"Your verification code is: {verificationCode.Code}",
                    ToPhoneNumber = createdUser.PhoneNumber
                });

            return true;
        }
    }
}
