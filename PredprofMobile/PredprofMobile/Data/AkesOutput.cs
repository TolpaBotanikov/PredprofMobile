using System;
using System.Collections.Generic;
using System.Text;

namespace PredprofMobile.Data
{
    public class AkesOutput
    {
        public int active_blocks { get; set; }
        public double active_power_A { get; set; }
        public double? active_power_A_off { get; set; }
        public double active_power_B { get; set; }
        public double? active_power_B_off { get; set; }
        public double active_power_C { get; set; }
        public double? active_power_C_off { get; set; }
        public int akes_id { get; set; }
        public double cos_A { get; set; }
        public double? cos_A_off { get; set; }
        public double cos_B { get; set; }
        public double? cos_B_off { get; set; }
        public double cos_C { get; set; }
        public double? cos_C_off { get; set; }
        public DateTime datetime_end { get; set; }
        public DateTime datetime_start { get; set; }
        public int id { get; set; }
        public double jet_power_A { get; set; }
        public double? jet_power_A_off { get; set; }
        public double jet_power_B { get; set; }
        public double? jet_power_B_off { get; set; }
        public double jet_power_C { get; set; }
        public double? jet_power_C_off { get; set; }
        public double voltage_A { get; set; }
        public double? voltage_A_off { get; set; }
        public double voltage_B { get; set; }
        public double? voltage_B_off { get; set; }
        public double voltage_C { get; set; }
        public double? voltage_C_off { get; set; }
        public double? effectiveness { get; set; }

        public AkesOutput(int active_blocks, 
            double active_power_A, double? active_power_A_off, 
            double active_power_B, double? active_power_B_off, 
            double active_power_C, double? active_power_C_off, 
            int akes_id, 
            double cos_A, double? cos_A_off, 
            double cos_B, double? cos_B_off, 
            double cos_C, double? cos_C_off, 
            DateTime datetime_end, DateTime datetime_start, 
            int id, double jet_power_A, double? jet_power_A_off,
            double jet_power_B, double? jet_power_B_off,
            double jet_power_C, double? jet_power_C_off,
            double voltage_A, double? voltage_A_off,
            double voltage_B, double? voltage_B_off, 
            double voltage_C, double? voltage_C_off)
        {
            this.active_blocks = active_blocks;
            this.active_power_A = active_power_A;
            this.active_power_A_off = active_power_A_off;
            this.active_power_B = active_power_B;
            this.active_power_B_off = active_power_B_off;
            this.active_power_C = active_power_C;
            this.active_power_C_off = active_power_C_off;
            this.akes_id = akes_id;
            this.cos_A = cos_A;
            this.cos_A_off = cos_A_off;
            this.cos_B = cos_B;
            this.cos_B_off = cos_B_off;
            this.cos_C = cos_C;
            this.cos_C_off = cos_C_off;
            this.datetime_end = datetime_end;
            this.datetime_start = datetime_start;
            this.id = id;
            this.jet_power_A = jet_power_A;
            this.jet_power_A_off = jet_power_A_off;
            this.jet_power_B = jet_power_B;
            this.jet_power_B_off = jet_power_B_off;
            this.jet_power_C = jet_power_C;
            this.jet_power_C_off = jet_power_C_off;
            this.voltage_A = voltage_A;
            this.voltage_A_off = voltage_A_off;
            this.voltage_B = voltage_B;
            this.voltage_B_off = voltage_B_off;
            this.voltage_C = voltage_C;
            this.voltage_C_off = voltage_C_off;
        }
    }
}
