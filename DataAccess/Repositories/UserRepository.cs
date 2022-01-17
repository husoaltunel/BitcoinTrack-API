using Dapper;
using DataAccess.Abstract;
using Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection Connection { get; set; }
        private IConfiguration _configuration { get; set; }
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection"));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Connection.QueryAsync<User>($@"select * from ""Users"" ");
        }

        public async Task<IEnumerable<User>> GetByFilterAsync(Func<User, bool> filter)
        {
            return await Task.FromResult(GetAllAsync().Result.Where(filter));
        }
    }
}
