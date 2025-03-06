using FluentValidation;

namespace Bookify.Application.Bookings.ReserveBooking;
public class ReserveBookingValidator : AbstractValidator<ReserveBookingCommand>
{
	public ReserveBookingValidator()
	{
		RuleFor(x => x.ApartmentId).NotEmpty();

		RuleFor(x => x.UserId).NotEmpty();

		RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
	}
}