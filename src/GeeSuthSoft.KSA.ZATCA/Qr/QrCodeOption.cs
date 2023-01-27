using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeeSuthSoft.KSA.ZATCA.Enums.Options;

namespace GeeSuthSoft.KSA.ZATCA.Qr
{
    public class QrCodeOption
    {
        public QrCodeOption()
        {
            this.Language = Language.En;
        }
        public Language Language = Enums.Options.Language.Ar;

        public string PointsColor { get; set; } = "#15463F";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public bool DrawQuietZones { get; set; } = true;

        public Bitmap CenterImage { get; set; }
        public int IconSizePercent { get; set; } = 30;
        public int IconBorderWidth { get; set; } = 6;


    }
}
