using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagePreviewTest
{
    public enum View
    {
        Exterior,
        Interior,
        DriversView,
        RearView
    }
    public static class Routes
    {
        public static Dictionary<View, string> V3 = new Dictionary<View, string>()
        {
            {
                View.Exterior, "vbsNext-v3/exterior/{StructureWeek}/{Model}/{Engine}/{Salesversion}/{Color}/{Rims}/{Bodykit}/{Rails}/{Spoiler}/{TintedWindows}/{Misc1}/{Misc2}/{Misc3}/{Misc4}/{Misc5}/default.png"
            },
            {
                View.Interior, "vbsNext-v3/interior/{StructureWeek}/{Model}/{Transmission}/{Upholstery}/{Trim}/{Gearlever}/{SteeringWheel}/{Rti}/{Dashboard}/{Ceiling}/default.png"
            },
            {
                View.DriversView, "vbsNext-v3/drivers-view/{StructureWeek}/{Model}/{Transmission}/{Upholstery}/{Trim}/{Gearlever}/{SteeringWheel}/{Rti}/{Dashboard}/{CombiInstrument}/{Unidecor}/{Lcd}/{Paddle}/{Ceiling}/default.png"
            }
        };

        public static Dictionary<View, string> V4 = new Dictionary<View, string>()
        {
            {
                View.Exterior, "vbsNext-v4/exterior/{StructureWeek}/{Model}/{Engine}/{Salesversion}/{Color}/{Rims}/{Bodykit}/{Rails}/{Spoiler}/{TintedWindows}/{Misc1}/{Misc2}/{Misc3}/{Misc4}/{Misc5}/{Headlights}/{EngineEmblem}/default.png"
            },
            {
                View.Interior, "vbsNext-v4/interior/{StructureWeek}/{Model}/{Transmission}/{Upholstery}/{Trim}/{Gearlever}/{SteeringWheel}/{Rti}/{Dashboard}/{Ceiling}/{SunRoof}/{SoundSystem}/default.png"
            },
            {
                View.DriversView, "vbsNext-v4/drivers-view/{StructureWeek}/{Model}/{Transmission}/{Upholstery}/{Trim}/{Gearlever}/{SteeringWheel}/{Rti}/{Dashboard}/{CombiInstrument}/{Unidecor}/{Lcd}/{Paddle}/{Ceiling}/{SunRoof}/{SoundSystem}/default.png"
            },
            {
                View.RearView,"vbsNext-v4/rear-view/{StructureWeek}/{Model}/{Upholstery}/{Ceiling}/{SunRoof}/default.png"
            }
        };
    }
}