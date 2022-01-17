using Dapper;
using DataAccess.Abstract;
using Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BitcoinDetailRepository : IBitcoinDetailRepository
    {
        private IDbConnection Connection { get;set;}
        private  IConfiguration _configuration { get;set;}
        public BitcoinDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection"));
        }
        public async Task<IEnumerable<BitcoinDetail>> GetAllAsync()
        {
            return await Connection.QueryAsync<BitcoinDetail>($@"select * from ""BitcoinDetails"" ");
        }

        public Task<int> InsertAsync(BitcoinDetail bitcoinDetail)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@price", bitcoinDetail.Price, DbType.Decimal);
            parameters.Add("@date", bitcoinDetail.Date, DbType.DateTime2);

            return Connection.ExecuteAsync($@"insert into ""BitcoinDetails"" (""Price"",""Date"") values(@price,@date) ", parameters);
        }
    }
}
