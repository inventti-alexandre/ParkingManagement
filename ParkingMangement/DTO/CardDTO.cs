﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMangement.DTO
{
    class CardDTO
    {
        private int identify;
        private string id;
        private string isUsing;
        private int type;
        private DateTime dayUnlimit;

        public int Identify { get => identify; set => identify = value; }
        public string Id { get => id; set => id = value; }
        public string IsUsing { get => isUsing; set => isUsing = value; }
        public int Type { get => type; set => type = value; }
        public DateTime DayUnlimit { get => dayUnlimit; set => dayUnlimit = value; }
    }
}
