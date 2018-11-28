using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTLMovieScraper.Models
{
    /// <summary>
    /// Used only for business purpose
    /// </summary>
    public class CastFromScraper
    {

        public int id { get; set; }
        public Person person { get; set; }

    }

    //public class Character
    //{
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int id { get; set; }
    //}

    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
    }
    /// <summary>
    /// Just for this project i did this. I could have done it better
    /// </summary>
    public class Cast
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
    }
}
