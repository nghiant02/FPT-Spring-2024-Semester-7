using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics_of_valedictorians
{
    using CsvHelper.Configuration.Attributes;

    public class Student
    {
        [Name("SBD")]
        public int SBD { get; set; }

        [Name("Toan")]
        public double? Toan { get; set; }

        [Name("Van")]
        public double? Van { get; set; }

        [Name("Ly")]
        public double? Ly { get; set; }

        [Name("Sinh")]
        public double? Sinh { get; set; }

        [Name("Ngoai ngu")]
        public double? NgoaiNgu { get; set; }

        [Name("Year")]
        public int Year { get; set; }

        [Name("Hoa")]
        public double? Hoa { get; set; }

        [Name("Lich su")]
        public double? LichSu { get; set; }

        [Name("Dia ly")]
        public double? DiaLy { get; set; }

        [Name("GDCD")]
        public double? GDCD { get; set; }

        [Name("MaTinh")]
        public int MaTinh { get; set; }
    }
}
