using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nghien_cuu_ung_dung
{
    internal class Du_bao : Doanh_thu
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }
}
