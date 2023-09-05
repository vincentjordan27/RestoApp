using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestoApp.Application;
using System.Data;

namespace RestoApp.Infrastructure.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(RestoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private readonly RestoDbContext dbContext;

        public List<Domain.Entities.Category> GetAll()
        {
            SqlConnection dbConnection = (SqlConnection)dbContext.Database.GetDbConnection();

            using (SqlCommand cmd = new SqlCommand("GetCategory", dbConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        Console.WriteLine(dataRow.ToString());
                    }

                }
                return new List<Domain.Entities.Category>();
            }
        }

        public async Task<bool?> CategoryExist(Guid id)
        {
            try
            {
                SqlConnection dbConnection = (SqlConnection)(dbContext.Database.GetDbConnection());

                using (SqlCommand cmd = new SqlCommand(SPRepository.SPCHECKCATEGORY, dbConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pID", SqlDbType.UniqueIdentifier)).Value = id;
                    DataTable dt = new DataTable();
                    await Task.Run(() => adapter.Fill(dt));
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dt.Rows) {
                            var count = dataRow["COUNT"];
                            if (count == null)
                            {
                                return false;
                            }
                            if ((int)count > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
