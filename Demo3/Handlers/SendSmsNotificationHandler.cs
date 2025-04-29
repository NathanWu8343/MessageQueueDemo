using MediatR;

namespace Demo3.Handlers
{
    public class SendSmsNotificationHandler : INotificationHandler<MemberUpgradedEvent>
    {
        public Task Handle(MemberUpgradedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[SMS] 發送會員 {notification.MemberId} 升等簡訊...");
            return Task.CompletedTask;
        }
    }
}