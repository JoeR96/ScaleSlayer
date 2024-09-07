namespace LoopLearner.Application.Songs.CreateSong;
using FluentValidation;

public class CreateSongCommandValidator : AbstractValidator<CreateSongCommand>
{
    public CreateSongCommandValidator()
    {
        RuleFor(s => s.Title).NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

        RuleFor(s => s.Artist).NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

        RuleFor(s => s.Genre).NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters");

        RuleFor(s => s.BPM).GreaterThan(0).WithMessage("BPM must be a positive number");

        RuleFor(s => s.InstrumentParts).NotNull().WithMessage("Instrument parts cannot be null")
            .SetValidator(new InstrumentPartsForCreateSongValidator());
    }
    
    public class InstrumentPartsForCreateSongValidator : AbstractValidator<IEnumerable<InstrumentPartCommand>>
    {
        public InstrumentPartsForCreateSongValidator()
        {
            RuleFor(ip => ip).ForEach(ip =>
            {
                ip.NotNull().WithMessage("Instrument part cannot be null")
                    .SetValidator(new InstrumentPartForCreateSongValidator());
            });
        }
    }

    public class InstrumentPartForCreateSongValidator : AbstractValidator<InstrumentPartCommand>
    {
        public InstrumentPartForCreateSongValidator()
        {
            RuleFor(ip => ip.InstrumentName).NotEmpty().WithMessage("Instrument name is required");
            RuleFor(ip => ip.Tabs).NotEmpty().WithMessage("Tabs are required");
        }
    }
}