using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StudentDetail
{
    public class StudentData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Mark1 { get; set; }
        public double Mark2 { get; set; }
        public double Mark3 { get; set; }
        public double Total { get; set; }
        public double Average { get; set; }
    }
    
    public class StudentDetailRepository
    {

        public IEnumerable<StudentData> GetData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConn"].ToString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id],[FirstName],[LastName],[Mark1],[Mark2],[Mark3],[Total],[Average]
               FROM [dbo].[StudentDetail]", connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new StudentData()
                            {
                                Id = x.GetInt32(0),
                                FirstName = x.GetString(1),
                                LastName = x.GetString(2),
                                Mark1 = x.GetDouble(3),
                                Mark2 = x.GetDouble(4),
                                Mark3 = x.GetDouble(5),
                                Total = x.GetDouble(6),
                                Average = x.GetDouble(7)
                            }).ToList();
                }
            }
        }
        
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
           new StudentHub().Show();
        }
    }
}