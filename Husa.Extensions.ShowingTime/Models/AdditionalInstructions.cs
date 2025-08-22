namespace Husa.Extensions.ShowingTime.Models
{
    using Husa.Extensions.ShowingTime.Interfaces;

    public class AdditionalInstructions : IAdditionalInstructions
    {
        public string NotesForApptStaff { get; set; }
        public string NotesForShowingAgent { get; set; }

        public AdditionalInstructions Clone() => this.MemberwiseClone() as AdditionalInstructions;
    }
}
