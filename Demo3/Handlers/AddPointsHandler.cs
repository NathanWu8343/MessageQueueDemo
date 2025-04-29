using MediatR;

namespace Demo3.Handlers
{
    public class AddPointsHandler : INotificationHandler<MemberUpgradedEvent>
    {
        public Task Handle(MemberUpgradedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Point] 會員 {notification.MemberId} 升等，加贈點數...");
            return Task.CompletedTask;
        }
    }
}