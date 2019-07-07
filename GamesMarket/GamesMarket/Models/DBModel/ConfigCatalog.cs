﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.DBModel
{
    public class ConfigCatalog
    {
        public int Id { get; set; }
        public int RAM { get; set; }
        public int CPU { get; set; }
        public int VideoCard { get; set; }
        public int OS { get; set; }
        public int SizeOnDisc { get; set; }
        public int GameId { get; set; }
    }
}