using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Models
{
    //DrawDate,Ball 1,Ball 2,Ball 3,Ball 4,Ball 5,Thunderball,Ball Set,Machine,DrawNumber
    //18-May-2024,26,3,27,38,36,2,T2,Excalibur 4,3489
    public class CSVModel
    {
        [Name("DrawDate")]
        public string Date { get; set; }

        [Name("Ball 1")]
        public int Ball1 { get; set; }

        [Name("Ball 2")]
        public int Ball2 { get; set; }

        [Name("Ball 3")]
        public int Ball3 { get; set; }

        [Name("Ball 4")]
        public int Ball4 { get; set; }

        [Name("Ball 5")]
        public int Ball5 { get; set; }

        [Name("Thunderball")]
        public int Thunderball { get; set; }
    }
}
