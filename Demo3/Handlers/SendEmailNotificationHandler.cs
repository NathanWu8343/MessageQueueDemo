using MediatR;

namespace Demo3.Handlers
{
    public class SendEmailNotificationHandler : INotificationHandler<MemberUpgradedEvent>
    {
        public Task Handle(MemberUpgradedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Email] 寄送會員 {notification.MemberId} 升等到 {notification.NewLevel} 的通知信...");
            return Task.CompletedTask;
        }
    }
}