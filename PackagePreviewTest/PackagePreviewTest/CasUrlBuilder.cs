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
    class CasUrlBuilder
    {
        public string ContentStructureWeek { get; private set; }
        public string Pno3 { get; private set; }
        private string RepoFolder { get; set; }

        private HashSet<FeatureType> requiredFeatures = new HashSet<FeatureType>();
        private Dictionary<FeatureType, string> featureCodes = new Dictionary<FeatureType, string>();

        public CasUrlBuilder(string contentStructureWeek, string pno3, string repoFolder)
        {
            ContentStructureWeek = contentStructureWeek;
            Pno3 = pno3;
            RepoFolder = repoFolder;
            requiredFeatures = Vcc.Cas.Common.Image.LayerValidation.GetModelSettings(ContentStructureWeek, int.Parse(Pno3.Substring(1))).Required;
        }

        public string GenerateUrl(string baseUrl, View view)
        {
            if(ContentStructureWeek.CompareTo("MY17_1617") < 0)
                return baseUrl + InsertFeatures(Routes.V3[view]);
            else
                return baseUrl + InsertFeatures(Routes.V4[view]);
        }

        private string InsertFeatures(string route)
        {
            foreach (var feature in featureCodes)
            {
                var tag = $"{{{feature.Key}}}";
                route = route.Replace(tag, feature.Key == FeatureType.StructureWeek ? feature.Value : feature.Value.Substring(1));
            }

            var regex = new Regex(@"{\w+}");
            return regex.Replace(route, "_");
        }

        public void SetFeatureToVisualize(string path)
        {
            featureCodes = PackageParser.FindDependantFeatures(path);
            GetDefaultFeatures();
        }

        private void GetDefaultFeatures()
        {
            var rootDir = Path.Combine(RepoFolder, ContentStructureWeek, Pno3);

            foreach (var feature in requiredFeatures.Where(x => !featureCodes.Keys.Contains(x)))
            {
                var defaultFeature = SearchFolders(rootDir, feature);
                featureCodes.Add(defaultFeature.Key, defaultFeature.Value);
            }
        }

        private KeyValuePair<FeatureType, string> SearchFolders(string rootDir, FeatureType type)
        {
            var folders = Directory.EnumerateDirectories(rootDir);

            var existingSubfolder = folders.FirstOrDefault(f => featureCodes.Values.Contains(Path.GetFileName(f)) && PackageParser.PartialPathMatch(f, type));

            if (!string.IsNullOrEmpty(existingSubfolder))
                SearchFolders(existingSubfolder, type);

            foreach (var folder in folders)
            {
                var folderMatch = PackageParser.FindFeatureInPath(folder, type);

                if (!string.IsNullOrEmpty(folderMatch))
                {
                    return new KeyValuePair<FeatureType, string>(type, folderMatch);
                }
                else if (PackageParser.PartialPathMatch(folder, type))
                {
                    return SearchFolders(folder, type);
                }
            }

            return default(KeyValuePair<FeatureType, string>);
        }
    }
}
