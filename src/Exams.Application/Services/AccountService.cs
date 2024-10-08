﻿using Exams.Application.Interfaces;
using Exams.Domain.Dtos;
using Exams.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Exams.Application.Services
{
    public class AccountService(
        IPasswordHasherService passwordHasherService,
        IUserService userService,
        IVerificationCodeService verificationCodeService,
        ISmsSenderService smsSenderService,
        ITokenGeneratorService tokenGeneratorService) : IAccountService
    {
        public async ValueTask<bool> SendResetCodeAsync(Guid userId)
        {
            var user = await userService.GetAll().FirstOrDefaultAsync(user => user.Id == userId);

            if(user is null)
            {
                return false;
            }

            var random = new Random();
            
            await smsSenderService.SendAsync(
                new SendSmsRequest
                {
                    Message = $"Your code for reset password is : {random.Next(10_000, 99_999)}",
                    ToPhoneNumber = user.PhoneNumber
                });

            return true;
        }


        public async ValueTask<bool> UpdatePasswordAsync(Guid userId, UpdatePasswordDetails updatePasswordDetails)
        {
            var user = await userService.GetAll().FirstOrDefaultAsync(user => user.Id == userId);

            if(user is null)
            {
                return false;
            }

            if(!passwordHasherService.Verify(updatePasswordDetails.OldPassword, user.Password))
            {
                throw new InvalidOperationException("Password is not correct!");
            }

            if(updatePasswordDetails.NewPassword.Length is < 8 or > 32)
            {
                throw new InvalidOperationException("Password must be between 8 and 32.");
            }

            if(updatePasswordDetails.NewPassword != updatePasswordDetails.ConfirmNewPassword)
            {
                throw new InvalidOperationException("Passwords don't match.");
            }

            var updatedUserDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Password = passwordHasherService.Hash(updatePasswordDetails.NewPassword),
                Role = user.Role,
                IsVerified = user.IsVerified
            };

            await userService.UpdateAsync(updatedUserDto, user.Id);

            return true;
        }

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

        public async ValueTask<bool> SendVerificationCodeAsync(Guid userId)
        {
            var user = await userService.GetByIdAsync(userId);

            if (user is null)
            {
                throw new InvalidOperationException("User not found with this phone number");
            }

            var random = new Random();

            var verificationCode = new VerificationCodeDto
            {
                Code = random.Next(10_000, 99_999).ToString(),
                UserId = user.Id
            };

            await verificationCodeService.CreateAsync(verificationCode);

            await smsSenderService.SendAsync(new SendSmsRequest
            {
                Message = $"Your new verification code is : {verificationCode.Code}",
                ToPhoneNumber = user.PhoneNumber
            });

            return true;
        }

        public async ValueTask<bool> VerifyCodeAsync(Guid userId, string code)
        {
            var verificationCode = await verificationCodeService.GetAll().OrderBy(code
                => code.CreatedDate).LastOrDefaultAsync();

            if (verificationCode is null)
            {
                return false;
            }

            if (verificationCode.Code != code)
            {
                return false;
            }

            var user = await userService.GetByIdAsync(userId);

            if (user is not null)
            {
                user.IsVerified = true;
                await userService.UpdateAsync(
                    new UserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Password = user.Password,
                        Role = user.Role,
                        IsVerified = true
                    }, user.Id);

                return true;
            }

            return false;
        }
    }
}
