﻿using BlogHostCore.DomainModels;

namespace BlogHostCore.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<UserInfo>
    {
        void Add(UserInfo model);
        UserInfo GetByName(string name);
        UserInfo GetByEmail(string email);
    }
}
