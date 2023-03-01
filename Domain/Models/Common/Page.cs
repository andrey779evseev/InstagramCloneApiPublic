namespace Domain.Models.Common;

public record Page<T>(
    bool HasNextPage,
    List<T> Items
);