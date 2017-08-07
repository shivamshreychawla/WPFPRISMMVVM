using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Vcc.Cas.Common;

namespace PackagePreviewTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl, INavigationAware
    {
        private string _title;

        public string Title { get => _title; set => _title = value; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        string baseUrl = @"http://cas.volvocars.com/image/";
        string testFolder = @"\\semal-fzhdj5j\E\CAS Assets\20170710C";
        string repoFolder = @"\\semal-fzhdj5j\E\FlatRepo_v2";
        string[] imgExtensions = { ".jpg", ".png" };

        public MainWindow()
        {
            DataContext = this;
            Title = "Package Preview";
            InitializeComponent();          
            TvPackagePreview.Items.Add(GetTreeView(testFolder));
        }

        private string FolderName(string path)
        {
            return Path.GetFileName(path);
        }

        private TreeViewItem GetTreeView(string testFolder)
        {
            var tvRoot = new TreeViewItem { Header = FolderName(testFolder) };
            tvRoot.IsExpanded = true;

            AddTreeViewItems(tvRoot, testFolder);

            return tvRoot;
        }

        private void AddTreeViewItems(TreeViewItem root, string path)
        {
            foreach (var dir in Directory.EnumerateDirectories(path))
            {
                var tvItem = new TreeViewItem { Header = FolderName(dir), Tag = dir };

                tvItem.MouseDoubleClick += TvItem_MouseDoubleClick;

                AddTreeViewItems(tvItem, dir);

                if (Directory.EnumerateFiles(dir).Any(x => imgExtensions.Contains(new FileInfo(x).Extension)))
                {
                    tvItem.IsExpanded = false;
                    tvItem.Background = Brushes.LightGreen;
                }
                else
                    tvItem.IsExpanded = true;

                root.Items.Add(tvItem);
            }
        }

        private CasUrlBuilder CreateUrlBuilder(string path)
        {
            var csw = PackageParser.FindFeatureInPath(path, FeatureType.StructureWeek);
            var pno3 = PackageParser.FindFeatureInPath(path, FeatureType.Model);

            var cub = new CasUrlBuilder(csw, pno3, repoFolder);
            cub.SetFeatureToVisualize(path);

            return cub;
        }

        private void TvItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tvi = sender as TreeViewItem;

            if (tvi != null && !tvi.IsSelected)
                return;

            e.Handled = true;

            var type = PackageParser.ParseFolder(tvi.Tag.ToString());

            if (type.HasValue)
            {
                var cub = CreateUrlBuilder(tvi.Tag.ToString());
                Process.Start("chrome.exe", cub.GenerateUrl(baseUrl, View.Exterior));
                //TbParsedFeature.Text = $"The folder \"{tvi.Tag}\" contains feature: {type.Value.ToString()}";
            }

            //Process.Start("explorer.exe", tvi.Tag.ToString());
        }

        private void TvPackagePreview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            LwContents.ItemsSource = Directory.EnumerateFiles(((TreeViewItem)(sender as TreeView).SelectedItem).Tag.ToString()).Select(x => new ListViewItem { Content = Path.GetFileName(x), Tag = x });
            LwContents.SelectedItem = LwContents.Items.Count > 0 ? LwContents.Items[0] : null;
        }

        private void LwContents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var path = ((ListViewItem)(sender as ListView).SelectedItem)?.Tag.ToString();

            if (path != null && imgExtensions.Contains(new FileInfo(path).Extension))
                ImgPreviewImage.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
        }
    }
}
