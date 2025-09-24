using System.Collections.Generic;
using System.Linq;

namespace Web_QLNT.Models
{
    public class CToHD
    {
        public HoaDon Hd { get; set; }
        public List<CTHoaDon> Ds { get; set; } = new List<CTHoaDon>();

        public string[] LayCTHD()
        {
            return Ds.Select(item => item.MaSP).ToArray();
        }
    }
}