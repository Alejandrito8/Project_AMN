namespace Project_AMN.Queries;
public record ListUsersQuery() : IRequest<IReadOnlyList<UserDto>>;