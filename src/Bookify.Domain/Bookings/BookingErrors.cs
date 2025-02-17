using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public static class BookingErrors
{
    public static Errors NotFound = new(
        "Booking.Found",
        "The booking with the specified identifier was not found");

    public static Errors Overlap = new(
        "Booking.Overlap",
        "The current booking is overlapping with an existing one");

    public static Errors NotReserved = new(
        "Booking.NotReserved",
        "The booking is not pending");

    public static Errors NotConfirmed = new(
        "Booking.NotReserved",
        "The booking is not confirmed");

    public static Errors AlreadyStarted = new(
        "Booking.AlreadyStarted",
        "The booking has already started");
}