using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisPlayerDomain.Entities
{
    public class TennisPlayer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string Sex { get; set; }
        public Country Country { get; set; }
        public string Picture { get; set; }
        public TennisPlayerInformation PlayerInformation { get; set; }
    }
    public class Country
    {
        public string Picture { get; set; }
        public string Code { get; set; }
    }
    public class  TennisPlayerInformation 
    {
        public int Rank { get; set; }
        public int Point { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; }
    }
}
