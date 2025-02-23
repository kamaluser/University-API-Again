using FluentValidation;

namespace UniversityAppAgain.Dtos.GroupDtos
{
    public class GroupUpdateDto
    {
        public string No { get; set; }
        public byte Limit { get; set; }
    }

    public class GroupUpdateDtoValidator : AbstractValidator<GroupUpdateDto>
    {
        public GroupUpdateDtoValidator()
        {
            RuleFor(x => x.No).NotEmpty().NotNull().MinimumLength(4).MaximumLength(5);
            RuleFor(x => x.Limit).NotNull().InclusiveBetween((byte)5, (byte)18);
        }
    }
}
