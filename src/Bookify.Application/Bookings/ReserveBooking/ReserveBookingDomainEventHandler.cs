﻿using Bookify.Application.Abstraction.Email;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;
internal sealed class ReserveBookingDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
	private readonly IUserRepository userRepository;
	private readonly IBookingRepository bookingRepository;
	private readonly IEmailService emailService;
	public ReserveBookingDomainEventHandler(
		IUserRepository userRepository,
		IBookingRepository bookingRepository,
		IEmailService emailService)
	{
		this.userRepository = userRepository;
		this.bookingRepository = bookingRepository;
		this.emailService = emailService;
	}
	public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
	{
		var booking = await bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

		if (booking is null)
		{
			return;
		}

		var user = await userRepository.GetByIdAsync(booking.UserId, cancellationToken);

		if (user is null)
		{
			return;
		}

		await emailService.SendEmail(user.Email,
			"Booking Reserved",
			"You have 10 minutes to confirm this booking");
	}
}