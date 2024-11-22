using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisPlayerApplication.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object ) for tennis player . 
    /// </summary>
    public class TennisPlayerDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Shortname { get; set; }
        public string Sex { get; set; }
        public string CountryCode { get; set; }
        public string CountryPicture { get; set; }
        public string Picture { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; } 
    }
}
