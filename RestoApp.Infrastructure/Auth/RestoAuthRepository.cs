using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestoApp.Application.Auth;
using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Infrastructure.Auth
{
    public class RestoAuthRepository : IRestoAuthRepository
    {
        private readonly RestoDbContext dbContext;
        private readonly ILogger<RestoAuthRepository> logger;

        public RestoAuthRepository(RestoDbContext dbContext, ILogger<RestoAuthRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<Resto?> InsertResto(Resto resto)
        {
            try
            {
                SqlConnection dbConnection = (SqlConnection)dbContext.Database.GetDbConnection();

                using (SqlCommand cmd = new SqlCommand(SPRepository.SPINSERTRESTO, dbConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pINS_UPT", SqlDbType.VarChar)).Value = "INS";
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = resto.Id;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pNAME", SqlDbType.VarChar)).Value = resto.Name;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pCATEGORYID", SqlDbType.UniqueIdentifier)).Value = resto.CategoryId;
                    DataTable dt = new DataTable();
                    await Task.Run(() => adapter.Fill(dt));
                    if (dt.Rows.Count > 0)
                    {
                        return resto;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"RestoAuthRepository Create Resto: {ex.Message}");
                return null;
            }
        }
    }
}
