using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;

namespace YatzeeWPF.Models
{
    class HighScoreManager
    {
       
        private SQLiteConnection con;

        public HighScoreManager()
        {
          
            con = new SQLiteConnection("Data Source=Yatzy.db;Version=3;New=False;Compress=True;");
            ExecuteNonQuery("create table if not exists HighScore(id integer primary key autoincrement, name varchar(50) not null, points integer not null);");
        }

        private void ExecuteNonQuery(string txtQuery)
        {
      
            try
            {
                con.Open();
                var com = con.CreateCommand();
                com.CommandText = txtQuery;
                com.ExecuteNonQuery();

            } catch(SQLiteException e)
            {
                throw new HighScoreException(e.Message);
            }
            
            con.Close();
        }  


        public HighScoreEntry[] GetHighScore()
        {

            List<HighScoreEntry> localList = new List<HighScoreEntry>();
            
            string stm = "select * from HighScore order by Points desc";

            try
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {

                            HighScoreEntry localEntry = new HighScoreEntry();
                            localEntry.Name = rdr["name"].ToString();
                            localEntry.Points = Convert.ToInt32(rdr["points"]);
                            localList.Add(localEntry);

                        }

                    }
                }
            }catch(SQLiteException e)
            {
                throw new HighScoreException(e.Message);
            }

            return localList.ToArray();
        }

        public void AddToHighScore(HighScoreEntry highScoreEntry)
        {
            try
            {
                con.Open();
                string stm = "Insert into HighScore (Name, Points) values (@Name,@Points) ";
                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("Name", highScoreEntry.Name);
                    cmd.Parameters.AddWithValue("Points", highScoreEntry.Points);
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }catch (SQLiteException e)
            {
                throw new HighScoreException(e.Message);
            }
        }


        





    }

}
