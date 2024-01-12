using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanzschule
{
    internal class Datenbank
    {
        static private MySqlConnection conn = null;

        static public void Open()
        {                                                           // TODO :
                                                                            //Datenbank erstellen
            conn = new MySqlConnection("Server=localhost;Uid=root;Pwd=;Database=tanzschule;"); 
            conn.Open();
        }
        public static MySqlCommand CreateCommand()
        {
            return conn.CreateCommand();
        }

        static public void Close()
        {
            conn.Close();
            conn = null;
        }
    }
}
