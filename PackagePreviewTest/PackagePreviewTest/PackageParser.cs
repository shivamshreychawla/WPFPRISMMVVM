using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vcc.Cas.Common;

namespace PackagePreviewTest
{
    class PackageParser
    {
        public static FeatureType? ParseFolder(string path)
        {
            foreach (var pattern in PackagePatterns.PatternToFolderType)
            {
                if (Regex.IsMatch(path, pattern.Key.ToString()))
                    return pattern.Value;
            }

            return null;
        }

        public static string FindFeatureInPath(string path, FeatureType type)
        {
            var folders = new Queue<string>(path.Split('\\'));
            var currentPath = "";

            while (folders.Any())
            {
                currentPath += "\\" + folders.Dequeue();
                if(Regex.IsMatch(currentPath, PackagePatterns.FolderTypeToPattern[type].ToString()))
                    return Path.GetFileName(currentPath);
            }
            return null;
        }

        public static bool PartialPathMatch(string path, FeatureType type)
        {
            var pattern = PackagePatterns.FolderTypeToPattern[type].Pieces;
            var currentPattern = "";

            while (pattern.Any())
            {
                currentPattern += pattern.Dequeue();
                if (Regex.IsMatch(path, currentPattern + "$"))
                    return true;
                currentPattern += @"\\";
            }
            return false;
        }

        public static Dictionary<FeatureType, string> FindDependantFeatures(string path)
        {
            var dict = new Dictionary<FeatureType, string>();

            var folders = new Queue<string>(path.Split('\\'));
            var currentPath = "";

            do
            {
                currentPath += "\\" + folders.Dequeue();
                foreach (var pattern in PackagePatterns.PatternToFolderType)
                {
                    if (Regex.IsMatch(currentPath, pattern.Key.ToString()) && Path.GetFileName(currentPath).Substring(1) != "00000")
                    {
                        dict[pattern.Value] = Path.GetFileName(currentPath);
                        break;
                    }
                }
            }
            while (folders.Any());

            return dict;
        }
    }
}
