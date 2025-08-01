namespace Husa.Extensions.ShowingTime.Interfaces
{
    public interface IProvideShowingTime : IShowingTime
    {
        bool UseShowingTime { get; set; }
    }
}
