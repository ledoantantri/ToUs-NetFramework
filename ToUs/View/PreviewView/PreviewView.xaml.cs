using System.Windows.Controls;
using ToUs.Models;
using ToUs.Resources.CustomControl;

namespace ToUs.View.PreviewView
{
    /// <summary>
    /// Interaction logic for PreviewView.xaml
    /// </summary>
    public partial class PreviewView : UserControl
    {
        public PreviewView()
        {
            InitializeComponent();

            foreach (DataScheduleRow i in AppConfig.SelectedRows)
            {
                BoxTimetableDetail boxTimetableDetail = new BoxTimetableDetail();
                boxTimetableDetail.SetValue(Grid.ColumnProperty, int.Parse(i.Class.DayInWeek) - 1);
                boxTimetableDetail.SetValue(Grid.RowProperty, int.Parse(i.Class.Lession.Substring(0, 1)));
                boxTimetableDetail.SetValue(Grid.RowSpanProperty, i.Class.Lession.Length);
                gridTimeTable.Children.Add(boxTimetableDetail);
            }
        }
    }
}