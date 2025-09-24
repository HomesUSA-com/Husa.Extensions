namespace Husa.Extensions.Common.Tests.Providers
{
    using AutoMapper;

    public class TestProfile : Profile
    {
        public TestProfile()
        {
            this.CreateMap<Source, Destination>();
        }
    }
}
