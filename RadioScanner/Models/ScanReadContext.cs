using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class ScanReadContext
    {
        public string ConnectionString { get; set; }

        public ScanReadContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<BlueToothRead> GetBluetoothReads()
        {
            var reads = new List<BlueToothRead>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT *
                    FROM bluetoothdata
                    WHERE TIMESTAMP > DATE_SUB(NOW(), INTERVAL 7 DAY)
                    ORDER BY TIMESTAMP DESC
                    limit 100", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reads.Add(new BlueToothRead()
                        {
                            Time = Convert.ToDateTime(reader["TimeStamp"]),
                            MACAddress = reader["MAC"].ToString(),
                            SignalStrength = Convert.ToInt32(reader["RSSI"]),
                            Vendor = reader["Vendor"].ToString(),
                            ClassOfDevice = reader["cod"].ToString()
                        });
                    }
                }

            }
            return reads;
        }

        public List<WifiRead> GetWifiReads()
        {
            var reads = new List<WifiRead>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT *
                    FROM wifiscan
                    WHERE TIMESTAMP > DATE_SUB(NOW(), INTERVAL 7 DAY)
                    ORDER BY TIMESTAMP DESC
                    limit 100", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reads.Add(new WifiRead()
                        {
                            Time = Convert.ToDateTime(reader["TimeStamp"]),
                            MACAddress = reader["MAC"].ToString(),
                            SignalStrength = Convert.ToInt32(reader["RSSI"]),
                            Vendor = reader["Vendor"].ToString(),
                            Type = reader["Type"].ToString() == "A" ? WifiReadType.AccessPoint : WifiReadType.Client
                        });
                    }
                }

            }
            return reads;
        }
    }
}
