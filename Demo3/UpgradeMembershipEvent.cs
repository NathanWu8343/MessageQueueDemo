using MediatR;

namespace Demo3
{
    public class MemberUpgradedEvent : INotification
    {
        public string MemberId { get; }
        public string NewLevel { get; }

        public MemberUpgradedEvent(string memberId, string newLevel)
        {
            MemberId = memberId;
            NewLevel = newLevel;
        }
    }
}