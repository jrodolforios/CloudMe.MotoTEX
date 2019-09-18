using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using System;

namespace CloudMe.Auth.Admin.Model
{
    public class CloudMeUserDto: UserDto<Guid>
    {
        public Guid IdCompany { get; set; }
    }
}
