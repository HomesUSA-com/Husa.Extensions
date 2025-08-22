namespace Husa.Extensions.ShowingTime.Enums
{
    using System.Runtime.Serialization;

    public enum AppointmentChangesNotificationOptions
    {
        [EnumMember(Value = "CONFIRM_DECLINE_CANCEL_CALLS")]
        ConfirmDeclinedAndCancelCalls,
        [EnumMember(Value = "ONLY_DECLINE_CANCEL")]
        DeclinedAndCancelCallsOnly,
    }
}
