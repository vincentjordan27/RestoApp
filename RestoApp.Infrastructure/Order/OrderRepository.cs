using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestoApp.Application.Resto;
using RestoApp.Domain.Entities;
using RestoApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Infrastructure.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrderRepository> logger;

        public OrderRepository(OrderDbContext dbContext, ILogger<OrderRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<string?> OrderMenu(Domain.Entities.Order order)
        {
            try
            {
                SqlConnection sqlConnection = (SqlConnection)dbContext.Database.GetDbConnection();

                using (SqlCommand cmd = new SqlCommand(SPRepository.SPIORDER, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = order.Id;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pRESTOID", SqlDbType.UniqueIdentifier)).Value = order.RestoId;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pCUSTOMERID", SqlDbType.UniqueIdentifier)).Value = order.CustomerId;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pSTATUSID", SqlDbType.UniqueIdentifier)).Value = order.StatusId;
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
                                return "Failed Creating Order";
                            } else
                            {
                                foreach(OrderItem item in order.Items)
                                {
                                    using (SqlCommand cmdItem = new SqlCommand(SPRepository.SPIORDERITEM, sqlConnection))
                                    {
                                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmdItem);
                                        adapter1.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                        adapter1.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = Guid.NewGuid();
                                        adapter1.SelectCommand.Parameters.Add(new SqlParameter("@pORDERID", SqlDbType.UniqueIdentifier)).Value = order.Id;
                                        adapter1.SelectCommand.Parameters.Add(new SqlParameter("@pMENUID", SqlDbType.UniqueIdentifier)).Value = item.MenuId;
                                        DataTable dataTable = new DataTable();
                                        await Task.Run(() =>
                                        {
                                            adapter1.Fill(dataTable);
                                        });
                                        if (dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow row2 in dt.Rows)
                                            {
                                                if (Convert.ToInt32(row2[0]) < 1)
                                                {
                                                    throw new Exception("Failed Creating Order");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                SqlConnection sqlConnection = (SqlConnection)dbContext.Database.GetDbConnection();

                using (SqlCommand roleback = new SqlCommand(SPRepository.SPROLEBACKORDER, sqlConnection))
                {
                    SqlDataAdapter adapter2 = new SqlDataAdapter(roleback);
                    adapter2.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter2.SelectCommand.Parameters.Add(new SqlParameter("@pORDERID", SqlDbType.UniqueIdentifier)).Value = order.Id;
                    DataTable dt = new DataTable();
                    await Task.Run(() =>
                    {
                        adapter2.Fill(dt);
                    });
                }
                logger.LogError("Error Order Repository : Order Menu ", ex.Message);
                return ex.Message.ToString();
            }
        }
    }
}
