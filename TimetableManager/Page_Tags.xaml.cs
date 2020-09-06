using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using TimetableManager.TagDAO;

namespace TimetableManager
{
    /// <summary>
    /// Interaction logic for Page_Tags.xaml
    /// </summary>
    public partial class Page_Tags : Page
    {
        public Page_Tags()
        {
            InitializeComponent();
            PopulateTableTag(TagDetailsDAO.getAll());
        }

        private void addtag_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = new Tag();
            tag.Tags = txttag.Text;

            TagDetailsDAO.inserttag(tag);
            PopulateTableTag(TagDetailsDAO.getAll());
            cleartags();
        }

        private void PopulateTableTag(List<Tag> list)
        {

            var observableList = new ObservableCollection<Tag>();
            list.ForEach(x => observableList.Add(x));

            listViewtag.ItemsSource = observableList;
        }

        private void cleartags()
        {
            txttag.Text = "";
        }

       

        private void deletetag_Click(object sender, RoutedEventArgs e)
        {
            Tag tg = (Tag)listViewtag.SelectedItem;
            TagDetailsDAO.deleteTags(tg.Tags);
            PopulateTableTag(TagDetailsDAO.getAll());
            cleartags();

        }

        private void updatetag_Click(object sender, RoutedEventArgs e)
        {
            Tag tgt = (Tag)listViewtag.SelectedItem;

            Tag tag = new Tag();
            tag.Tags = txttag.Text;

            TagDetailsDAO.updateTag(tgt.Tags, tag);

            PopulateTableTag(TagDetailsDAO.getAll());
        }

        private void listViewtag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cleartags();

            Tag tag = (Tag)listViewtag.SelectedItem;

            if (tag != null)
                txttag.Text = tag.Tags;
        }

        private void searchFieldtag_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txttag.Text.Equals(""))
            {
                PopulateTableTag(TagDetailsDAO.getAll());
            }
            else
            {
                PopulateTableTag(TagDetailsDAO.searchTags(txttag.Text));
            }
        }

        private void cleartag_Click(object sender, RoutedEventArgs e)
        {
            cleartags();
        }
    }
}
