using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.BLModel
{
    public class ConfigCatalog
    {
        public int Id { get; set; }
        public int RAM { get; set; }
        public int SizeRam { get; set; }
        public int CPU { get; set; }
        public string Model { get; set; }
        public int VideoCard { get; set; }

        public string ModelVC { get; set; }
        public int SizeVC { get; set; }

        public int OS { get; set; }
        public string Name { get; set; }
        public string VersionOS { get; set; }
        public int SizeOnDisc { get; set; }
        public int GameId { get; set; }
    }
}