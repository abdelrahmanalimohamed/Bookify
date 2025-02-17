using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public static class ApartmentErrors
{
    public static Errors NotFound = new(
        "Apartment.NotFound",
        "The apartment with the specified identifier was not found");
}