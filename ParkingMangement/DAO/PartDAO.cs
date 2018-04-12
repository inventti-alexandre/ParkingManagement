﻿using ParkingMangement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMangement.DAO
{
    class PartDAO
    {
        public static DataTable GetAllData()
        {
            string sql = "select * from [Part]";
            return Database.ExcuQuery(sql);
        }

        public static string GetPartNameByPartID(string partID)
        {
            string sql = "select * from [Part] where PartID = '" + partID + "'";
            DataTable data = Database.ExcuQuery(sql);
            if (data != null)
            {
                return data.Rows[0].Field<string>("PartName");
            }
            return "";
        }

        public static void Insert(PartDTO partDTO)
        {
            string sql = "insert into Part(PartID, PartName, Sign, Amount, Limit) values ('" + partDTO.Id + "', '" + partDTO.Name + "', '" +
                partDTO.Sign + "', " + partDTO.Amount + ", " + partDTO.Limit + ")";
            Database.ExcuNonQuery(sql);
        }

        public static void Update(PartDTO partDTO)
        {
            string sql = "update Part set Name =('" + partDTO.Name + "'), Sign =('" + partDTO.Sign + "'), Amount =(" + partDTO.Amount + "), Limit =("
                + partDTO.Limit + ") where PartID =" + partDTO.Id + "";
            Database.ExcuNonQuery(sql);
        }

        public static void Delete(string partID)
        {
            string sql = "delete from [Part] where PartID = '" + partID + "'";
            Database.ExcuNonQuery(sql);
        }
    }
}
