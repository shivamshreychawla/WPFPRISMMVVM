using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcc.Cas.Common;

namespace PackagePreviewTest
{
    class Pattern
    {
        private Queue<string> _pieces;
        public Queue<string> Pieces
        {
            get
            {
                return new Queue<string>(_pieces.ToArray());
            }
            private set
            {
                _pieces = value;
            }
        }

        public Pattern(params string[] pieces)
        {
            Pieces = new Queue<string>(pieces);
        }

        public override string ToString()
        {
            return PackagePatterns.RegJoin(Pieces.ToArray());
        }
    }

    class PackagePatterns
    {
        static string featurePattern = JoinAlternatives(@"f[^\\]+", @"fA\d{5}", "ffallback");
        //static string upholsteryPattern = JoinAlternatives("u[A-Z0-9]{6}(_f[A-Z0-9]{2})?", "u00000", "ufallback");
        static string upholsteryPattern = @"u[A-Z0-9]{6}(_f[A-Z0-9]{2})?";
        static string cswPattern = @"MY\d{2}_\d{4}";
        static string modelPattern = @"m\d{3}";
        static string colorPattern = @"c(\d)(?!\1+)\d{4}";
        static string noColorPattern = @"c00000";
        static string versionPattern = @"v\w+";
        static string enginePattern = @"f[A-Z0-9]";
        static string interiorPattern = "interior";
        static string miscPattern = "misc";
        static string badgesPattern = "badges";
        static string windowsPattern = "windows";
        static string railsPattern = "rails";

        public static Dictionary<Pattern, FeatureType> PatternToFolderType = new Dictionary<Pattern, FeatureType>()
        {
            {
                new Pattern(cswPattern + "$"), FeatureType.StructureWeek
            },
            {
                new Pattern(cswPattern, modelPattern + "$"), FeatureType.Model
            },
            {
                new Pattern(cswPattern, modelPattern, colorPattern + "$"), FeatureType.Color
            },
            {
                new Pattern(cswPattern, modelPattern, colorPattern, "spoiler", featurePattern + "$"), FeatureType.Spoiler
            },
            {
                new Pattern(cswPattern, modelPattern, colorPattern, "version", versionPattern + "$"), FeatureType.Salesversion
            },
            {
                new Pattern(cswPattern, modelPattern, noColorPattern, "headlights", featurePattern + "$"), FeatureType.Headlights
            },
            {
                new Pattern(cswPattern, modelPattern, noColorPattern, "rim", featurePattern + "$"), FeatureType.Rims
            },
            {
                new Pattern(cswPattern, modelPattern, noColorPattern, miscPattern, featurePattern + "$"), FeatureType.Misc1
            },
            {
                new Pattern(cswPattern, modelPattern, colorPattern, miscPattern, featurePattern + "$"), FeatureType.Misc3
            },
            {
                new Pattern(cswPattern, modelPattern, noColorPattern, badgesPattern, featurePattern + "$"), FeatureType.EngineEmblem
            },
            {
                new Pattern(cswPattern, modelPattern, noColorPattern, windowsPattern, featurePattern + "$"), FeatureType.TintedWindows
            },
            {
                new Pattern(cswPattern, modelPattern, noColorPattern, railsPattern, featurePattern + "$"), FeatureType.Rails
            },
            {
                new Pattern(cswPattern, modelPattern, interiorPattern, upholsteryPattern + "$"), FeatureType.Upholstery
            },
            {
                new Pattern(cswPattern, modelPattern, interiorPattern, upholsteryPattern, "steeringwheel", featurePattern + "$"), FeatureType.SteeringWheel
            },
            {
                new Pattern(cswPattern, modelPattern, interiorPattern, upholsteryPattern, "inlays", featurePattern + "$"), FeatureType.Trim
            },
            {
                new Pattern(cswPattern, modelPattern, interiorPattern, upholsteryPattern, "ceiling", featurePattern + "$"), FeatureType.Ceiling
            },
            {
                new Pattern(cswPattern, modelPattern, interiorPattern, upholsteryPattern, "gearlever", enginePattern + "$"), FeatureType.Gearbox
            },
            {
                new Pattern(cswPattern, modelPattern, interiorPattern, upholsteryPattern, "gearlever", enginePattern, featurePattern, featurePattern + "$"), FeatureType.Gearlever
            },
        };

        public static Dictionary<FeatureType, Pattern> FolderTypeToPattern
        {
            get
            {
                return PatternToFolderType.ToDictionary(x => x.Value, x => x.Key);
            }
        }

        public static string RegJoin(params string[] patterns)
        {
            return string.Join("\\\\", patterns);
        }
        private static string JoinAlternatives(params string[] alts)
        {
            return $"{string.Join("|", alts.Select(x => $"({x})").ToArray())}";
        }
    }
}
