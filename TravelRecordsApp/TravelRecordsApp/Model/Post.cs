using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordsApp.Model
{
    public class Post
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Experience { get; set; }
        public string VenueName { get; set; }
        public string Catagoryid { get; set; }
        public string Catagoryname { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
    }
}
