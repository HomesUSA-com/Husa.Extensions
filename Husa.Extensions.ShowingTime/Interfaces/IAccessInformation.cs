namespace Husa.Extensions.ShowingTime.Interfaces
{
    using Husa.Extensions.ShowingTime.Enums;

    public interface IAccessInformation
    {
        string GateCode { get; set; }
        AccessMethod? AccessMethod { get; set; }
        string Location { get; set; }
        string Serial { get; set; }
        string Combination { get; set; }
        string SharingCode { get; set; }
        string CbsCode { get; set; }
        string Code { get; set; }
        string DeviceId { get; set; }
        string AccessNotes { get; set; }
        bool ProvideAlarmDetails { get; set; }
        bool HasManageKeySets { get; set; }
        string AlarmDisarmCode { get; set; }
        string AlarmArmCode { get; set; }
        string AlarmPasscode { get; set; }
        string AlarmNotes { get; set; }
    }
}
