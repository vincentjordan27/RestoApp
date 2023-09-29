using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestoApp.Application.Resto;
using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Infrastructure.Resto
{
    public class RestoRepository : IRestoRepository
    {
        private readonly RestoDbContext dbContext;
        private readonly ILogger<RestoRepository> logger;

        public RestoRepository(RestoDbContext dbContext, ILogger<RestoRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<(List<Menu>, string?)> GetRestoMenu(Guid id)
        {
            List<Menu> rows = new List<Menu>();
            try
            {
                SqlConnection sqlConnection = (SqlConnection)dbContext.Database.GetDbConnection();

                using (SqlCommand cmd = new SqlCommand(SPRepository.SPGETRESTOMENU, sqlConnection))
                {
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pRESTOID", SqlDbType.UniqueIdentifier)).Value = id;
                    DataTable dt = new DataTable();
                    await Task.Run(() =>
                    {
                        adapter.Fill(dt);
                    });
                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow row in dt.Rows)
                        {
                            Menu menu = new Menu();
                            menu.Id = Guid.Parse(row[0].ToString());
                            menu.Name = row[1].ToString();
                            menu.Price = Convert.ToInt32(row[2]);
                            menu.RestoId = Guid.Parse(row[3].ToString());
                            rows.Add(menu);
                            
                        }
                    }
                    logger.LogError(rows.ToString());
                    return (rows, null);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"RestoAuthRepository Create Resto: {ex.Message}");
                return (rows, ex.ToString());
            }
        }

        public async Task<string?> AddRestoMenu(Menu menu)
        {
            try
            {
                SqlConnection dbConnection = (SqlConnection)dbContext.Database.GetDbConnection();
                using (SqlCommand cmd = new SqlCommand(SPRepository.SPIURESTOMENU, dbConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pINS_UPT_DEL", SqlDbType.VarChar)).Value = "INS";
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = menu.Id;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pNAME", SqlDbType.VarChar)).Value = menu.Name;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pPRICE", SqlDbType.Int)).Value = menu.Price;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pRESTOID", SqlDbType.UniqueIdentifier)).Value = menu.RestoId;
                    DataTable dt = new DataTable();
                    await Task.Run(() =>
                    {
                        adapter.Fill(dt);
                    });
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToInt32(row[0]) < 1)
                            {
                                return "Failed Insert New Menu";
                            }
                        }
                    }
                };
                return null;
            }
            catch (Exception ex) {
                logger.LogError($"RestoRepository Add Menu Resto: {ex.Message}");
                return "Terjadi Kesalahan pada server";
            }
        }

        public async Task<string?> UpdateRestoMenu(Menu menu)
        {
            try
            {
                SqlConnection dbConnection = (SqlConnection)dbContext.Database.GetDbConnection();
                using(SqlCommand cmd = new SqlCommand(SPRepository.SPIURESTOMENU, dbConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pINS_UPT_DEL", SqlDbType.VarChar)).Value = "UPT";
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = menu.Id;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pNAME", SqlDbType.VarChar)).Value = menu.Name;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pPRICE", SqlDbType.Int)).Value = menu.Price;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pRESTOID", SqlDbType.UniqueIdentifier)).Value = menu.RestoId;
                    DataTable dt = new DataTable();
                    await Task.Run(() =>
                    {
                        adapter.Fill(dt);
                    });
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToInt32(row[0]) < 1)
                            {
                                return "Not Found";
                            }
                        }
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                logger.LogError($"RestoRepository Update Menu Resto: {ex.Message}");
                return "Terjadi Kesalahan pada server";
            }
        }

        public async Task<string?> DeleteRestoMenu(Guid id, Guid userId)
        {
            try
            {
                SqlConnection dbConnection = (SqlConnection)dbContext.Database.GetDbConnection();
                using (SqlCommand cmd = new SqlCommand(SPRepository.SPIURESTOMENU, dbConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pINS_UPT_DEL", SqlDbType.VarChar)).Value = "DEL";
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = id;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pNAME", SqlDbType.VarChar)).Value = "";
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pPRICE", SqlDbType.Int)).Value = 0;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pRESTOID", SqlDbType.UniqueIdentifier)).Value = userId;
                    DataTable dt = new DataTable();
                    await Task.Run(() =>
                    {
                        adapter.Fill(dt);
                    });

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToInt32(row[0]) < 1)
                            {
                                return "Not Found";
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"RestoRepository Delete Menu Resto: {ex.Message}");
                return "Terjadi Kesalahan pada server";
            }
        }
    }
}
