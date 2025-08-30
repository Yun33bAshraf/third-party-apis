using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using Dapper;

namespace OpenProfileAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbRepository _dbRepository;

    public UserRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<IEnumerable<UserDto>> GetUsers(int pageNo, int pageSize)
    {
        var dynamicParameters = new DynamicParameters();
        //dynamicParameters.Add("@p_user_id", pageNo);
        dynamicParameters.Add("@p_page_no", pageNo);
        dynamicParameters.Add("@p_page_size", pageSize);

        var users = await _dbRepository.QueryAsync<UserDto>("user_get_all_paginated", dynamicParameters, System.Data.CommandType.StoredProcedure);
        return users;
    }
}
