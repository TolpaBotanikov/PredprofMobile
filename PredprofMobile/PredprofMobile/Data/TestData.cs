using System;
using System.Collections.Generic;
using System.Text;

namespace PredprofMobile.Data
{
    public class TestData
    {
        public TestData(int id, string name, DateTime date)
        {
            Id = id;
            Name = name;
            Date = date;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
