﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMangement.DAO
{
    class CardTypeDAO
    {
        public static DataTable GetAllData()
        {
            string sql = "select * from [CardType] order by CardTypeID asc";
            return Database.ExcuQuery(sql);
        }

        public static string GetTypeNameByTypeID(string cardTypeID)
        {
            string sql = "select * from [CardType] where CardTypeID = '" + cardTypeID + "'";
            DataTable data = Database.ExcuQuery(sql);
            if (data != null)
            {
                return data.Rows[0].Field<string>("CardTypeName");
            }
            return "";
        }
    }
}