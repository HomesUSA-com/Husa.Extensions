namespace Husa.Quicklister.Extensions.Domain.Entities.Base
{
    using System;

    public interface IProvideUserInfo
    {
        Guid? SysCreatedBy { get; set; }

        Guid? SysModifiedBy { get; set; }

        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }
    }
}
