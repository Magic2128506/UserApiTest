using FluentValidation;
using UserApi.Contract.Requests.UserRequests;

namespace UserApi.Requests.UserRequests
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.User)
                .NotEmpty();
            RuleFor(x => x.User.Name)
                .NotEmpty().MaximumLength(100);
            RuleFor(x => x.User.SurName)
                .NotEmpty().MaximumLength(100);
            RuleFor(x => x.User.PhoneNumber)
                .NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(x => x.User.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");
        }
    }
}
