﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace WindowsFormsApplication1
{
    class db
    {
        public static OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database.accdb");
        public static void baglan()
        {
            if (baglanti.State != ConnectionState.Open)
                baglanti.Open();
        }
        public static void kes()
        {
            if (baglanti.State != ConnectionState.Closed)
                baglanti.Close();
        }
    }
}
