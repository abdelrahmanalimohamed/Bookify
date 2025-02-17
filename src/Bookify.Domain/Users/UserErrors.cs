using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users;

public static class UserErrors
{
    public static Errors NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");

    public static Errors InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
}