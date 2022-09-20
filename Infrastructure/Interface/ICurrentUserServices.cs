namespace SchedulingApplication.Infrastructure.Interface
{
    public interface ICurrentUserServices
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Name { get; }
        string Email { get; }
    }
}
