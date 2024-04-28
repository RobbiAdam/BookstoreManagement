﻿using Bookstore.Contract.Requests.User;
using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(CreateUserRequest request);
        Task<string> LoginAsync(LoginUserRequest request);
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
    }
}
