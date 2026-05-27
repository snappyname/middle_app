using FluentValidation;
using SmartDoorSensor.DTO;

namespace SmartDoorSensor.Validators.DTOs;

public class SetDoorStatusRequestValidator : AbstractValidator<SetDoorStatusDTO>
{
    public SetDoorStatusRequestValidator()
    {
        RuleFor(x => x.DoorStatus)
            .IsInEnum()
            .WithMessage("Invalid door status. Allowed: Opened, Closed.");
    }
}
