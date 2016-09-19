using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StudentDetail
{
    public class StudentHub : Hub
    {
        public void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<StudentHub>();
            context.Clients.All.displayStatus();
        }

        public void Send(string profileId, string message)
        {
            //Clients.User(profileId).send(message);
        }

        [HubMethodName("sendData")]
        public void SendData()
        {
             IEnumerable<StudentData> studentDataCollection = new List<StudentData>();

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
                        studentDataCollection = reader.Cast<IDataRecord>()
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

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<StudentHub>();
            context.Clients.All.RecieveData(studentDataCollection);
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                StudentHub nHub = new StudentHub();
                nHub.SendData();
            }
        }
    }
}