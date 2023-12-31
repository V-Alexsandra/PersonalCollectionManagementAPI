﻿using FluentValidation;
using PersonalCollectionManagement.Business.DTOs.UserDtos;

public class RegisterUserDtoValidator<T> : AbstractValidator<T> where T : RegisterUserDto
{
    public RegisterUserDtoValidator()
    {
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.\(\)\[\]\{\}\-\+=]+").WithMessage("Your password must contain at least one of these special characters: ! ? * . ( ) [ ] { } - + =");

        RuleFor(r => r.RepeatPassword)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.\(\)\[\]\{\}\-\+=]+").WithMessage("Your password must contain at least one of these special characters: ! ? * . ( ) [ ] { } - + =");

        RuleFor(r => r.Email)
            .NotNull().WithMessage("Email is required field");

        When(r => r.Email is not null, () =>
        {
            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("The entered value is not an email.");
        });
    }
}
