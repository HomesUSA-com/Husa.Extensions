namespace Husa.Extensions.ShowingTime.Models
{
    using Husa.Extensions.ShowingTime.Enums;
    using Husa.Extensions.ShowingTime.Interfaces;

    public class AccessInformation : IAccessInformation
    {
        public string GateCode { get; set; }
        public AccessMethod? AccessMethod { get; set; }
        public string Location { get; set; }
        public string Serial { get; set; }
        public string Combination { get; set; }
        public string SharingCode { get; set; }
        public string CbsCode { get; set; }
        public string Code { get; set; }
        public string DeviceId { get; set; }
        public string AccessNotes { get; set; }
        public bool ProvideAlarmDetails { get; set; }
        public bool HasManageKeySets { get; set; }
        public string AlarmDisarmCode { get; set; }
        public string AlarmArmCode { get; set; }
        public string AlarmPasscode { get; set; }
        public string AlarmNotes { get; set; }

        public AccessInformation Clone() => this.MemberwiseClone() as AccessInformation;
    }
}
