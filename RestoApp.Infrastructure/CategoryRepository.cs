using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestoApp.Application;
using RestoApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(RestoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private readonly RestoDbContext dbContext;

        public List<Category> GetAll()
        {
            SqlConnection dbConnection = (SqlConnection)dbContext.Database.GetDbConnection();

            using (SqlCommand cmd = new SqlCommand("GetCategory", dbConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        Console.WriteLine(dataRow.ToString());
                    }
                    
                }
                return new List<Category>();
            }
        }
            
    }
}
