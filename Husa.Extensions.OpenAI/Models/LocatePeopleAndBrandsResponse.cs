namespace Husa.Extensions.OpenAI.Models
{
    public class LocatePeopleAndBrandsResponse : LocatePeopleAndBrandsRequest
    {
        public bool ContainsPersons { get; set; }

        public bool ContainsBranding { get; set; }

        public string Description { get; set; }

        public bool IsValid => !this.ContainsPersons && !this.ContainsBranding;
    }
}
