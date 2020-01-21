﻿using Microsoft.AspNetCore.Identity;
using SargeStoreDomain.Entities.Identity;

namespace SargeStore.Interfaces.Services
{
    public interface IRoleClient : IRoleStore<Role> { }
}
