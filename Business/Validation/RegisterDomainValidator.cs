using Business.Domain.Auth;
using FluentValidation;

namespace Business.Validation
{
    public class RegisterDomainValidator : AbstractValidator<RegisterDomain>
    {
        public RegisterDomainValidator()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MinimumLength(3).WithMessage("Full Name must be at least 3 characters long.")
                .MaximumLength(200).WithMessage("Full Name cannot exceed 150 characters.");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("User Name is required.")
                .MinimumLength(2).WithMessage("User Name must be at least 4 characters long.")
                .MaximumLength(50).WithMessage("User Name cannot exceed 50 characters.");
            // NOTE: You would add an Async rule here to check for uniqueness in the database.

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
            // NOTE: You would add an Async rule here to check if the email is already in use.

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithMessage("Confirmation Password is required.")
                .Equal(user => user.Password).WithMessage("The password and confirmation password do not match.");
        }
    }
}
