namespace Husa.Extensions.UserInfo.Interfaces
{
    using System;

    public interface IUserEntity
    {
        Guid Id { get; set; }

        Guid? SysCreatedBy { get; set; }

        Guid? SysModifiedBy { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
