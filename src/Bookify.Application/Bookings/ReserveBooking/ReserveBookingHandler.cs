using Bookify.Application.Abstraction.Clock;
using Bookify.Application.Abstraction.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.ReserveBooking;
public class ReserveBookingHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
	private readonly IUserRepository userRepository;
	private readonly IApartmentRepository apartmentRepository;
	private readonly IBookingRepository bookingRepository;
	private readonly PricingService pricingService;
	private readonly IUnitOfWork unitOfWork;
	private readonly IDateTimeProvider dateTimeProvide;
	public ReserveBookingHandler(
		IUserRepository userRepository,
		IApartmentRepository apartmentRepository,
		IBookingRepository bookingRepository,
		PricingService pricingService,
		IUnitOfWork unitOfWork,
		IDateTimeProvider dateTimeProvide)
	{
		this.userRepository = userRepository;
		this.apartmentRepository = apartmentRepository;
		this.bookingRepository = bookingRepository;
		this.pricingService = pricingService;
		this.unitOfWork = unitOfWork;
		this.dateTimeProvide = dateTimeProvide;
	}
	public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByIdAsync(request.UserId);

		if (user is null)
		{
			return Result.Failure<Guid>(UserErrors.NotFound);
		}

		var apartment = await apartmentRepository.GetByIdAsync(request.ApartmentId);

		if (apartment is null)
		{
			return Result.Failure<Guid>(ApartmentErrors.NotFound);
		}

		var duration = DateRange.Create(request.StartDate, request.EndDate);

		if (await bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
		{
			return Result.Failure<Guid>(BookingErrors.Overlap);
		}

		var booking = Booking.Reserve(apartment,
			request.UserId,
			duration,
			dateTimeProvide.UtcNow,
			pricingService);

		bookingRepository.Add(booking);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return booking.Id;
	}
}
