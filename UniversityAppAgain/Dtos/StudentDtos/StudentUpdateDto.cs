using FluentValidation;

namespace UniversityAppAgain.Dtos.StudentDtos
{
    public class StudentUpdateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int GroupId { get; set; }
    }

    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.BirthDate).NotEmpty()
                .Must(BeAValidDate).WithMessage("Birth date must be a valid date.")
                .Must(BeWithinValidAgeRange).WithMessage("Student age must be between 10 and 100 years.");

            RuleFor(x => x.GroupId).NotEmpty().GreaterThan(0);
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool BeWithinValidAgeRange(DateTime date)
        {
            var today = DateTime.Today;
            var age = today.Year - date.Year;
            if (date.Date > today.AddYears(-age)) age--;
            return age >= 10 && age <= 100;
        }
    }
}
