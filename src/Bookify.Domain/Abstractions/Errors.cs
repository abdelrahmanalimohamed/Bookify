namespace Bookify.Domain.Abstractions;

public record Errors(string Code, string Name)
{
    public static Errors None = new(string.Empty, string.Empty);

    public static Errors NullValue = new("Error.NullValue", "Null value was provided");
}