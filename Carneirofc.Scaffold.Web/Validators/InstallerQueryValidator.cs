using Carneirofc.Scaffold.Web.Controllers.DTO;
using FluentValidation;
namespace Carneirofc.Scaffold.Web.Validators
{
    public class InstallerQueryValidator : AbstractValidator<InstallerQueryDto>
    {
        public InstallerQueryValidator()
        {
            RuleFor(x => x.Path).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Path).Must(x =>
            {
                return !x.Contains("..");

            }).WithMessage("{PropertyName} cannot contain (..)");

            RuleFor(x => x.Filter).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }

}