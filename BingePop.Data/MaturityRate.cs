using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingePop.Data
{
    public enum MatureAudRate
    {
        G = 1,
        PG,
        PG_13,
        R,
        NC_17,
        TV_MA,
        NR
    }

    public class MaturityRate
    {
        [Required]
        public MatureAudRate MatureRate { get; set; }
    }
}
