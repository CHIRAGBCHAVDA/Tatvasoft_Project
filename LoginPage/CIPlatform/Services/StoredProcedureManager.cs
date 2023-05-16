using CIPlatform.Models;
using Microsoft.Data.SqlClient;

namespace CIPlatform.Services
{
    public static class StoredProcedureManager
    {
        private static IConfiguration Configuration { get; }

        static StoredProcedureManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }


        private static string ConnectionString
        {
            get { return Configuration.GetConnectionString("ConnectionString"); }
        }

        #region Stored Proc For Getting User Pref
        public static List<int> GetNotificationPrefIdsByUserId(long UserId)
        {
            List<int> notificationPrefIds = new List<int>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using(SqlCommand command = new SqlCommand("spGetNotificationPrefIdByUserId",connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);

                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int notificationPrefId = Convert.ToInt32(reader["NotificationId"]);
                            notificationPrefIds.Add(notificationPrefId);
                        }
                    }
                }

            }
            return notificationPrefIds;

        }

        #endregion

        #region Stroed Proc For All User Notifications
        public static List<UserNotification> GetUserNotifications(long UserId)
        {
            List<UserNotification> notifications = new List<UserNotification>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetUserNotifications", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserNotification userNotification = new UserNotification()
                            {
                                UserNotificationId = (long)reader["UserNotificationId"],
                                UserId = (long)reader["UserId"],
                                NotificationId = (int)reader["NotificationId"],
                                NotificationText = (string)reader["NotificationText"],
                                NotificationAnchorId = (long)reader["NotificationAnchorId"],
                                NotificationDate = (DateTime)reader["NotificationDate"],
                                MarkedAsRead = (byte)reader["MarkedAsRead"],
                            };
                            notifications.Add(userNotification);
                        }
                    }
                }

            }
            return notifications;

        }


        #endregion




    }
}
