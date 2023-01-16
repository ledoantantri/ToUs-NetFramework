using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ToUs.Models;
using ToUs.Utilities;

namespace ToUs.ViewModel.ScheduleViewModel
{
    internal class NormalScheduleViewModel : ViewModelBase
    {
        private ObservableCollection<DataScheduleRow> _dataRows;

        public ICollectionView DataRowsView { get; }

        private string _textFilter = string.Empty;

        public ICommand CheckItemCommand { get; set; }

        public string TextFilter
        {
            get { return _textFilter; }
            set
            {
                _textFilter = value;
                OnPropertyChanged(nameof(TextFilter));
                DataRowsView.Refresh();
            }
        }

        public ObservableCollection<DataScheduleRow> DataRows
        {
            get
            {
                if (_dataRows != null)
                {
                    return _dataRows;
                }
                return null;
            }
            set
            {
                _dataRows = value;
                OnPropertyChanged(nameof(DataRows));
            }
        }

        public NormalScheduleViewModel()
        {
            //if (AppConfig.CurrentExcelPath != ExcelReader.FilePath)
            //{
            //    AppConfig.CurrentExcelPath = ExcelReader.FilePath;
            //    AppConfig.AllRows = DataQuery.GetAllDataRows();
            //}
            //if (!String.IsNullOrEmpty(AppConfig.CurrentExcelPath))
            //{
            //    DataRows = new ObservableCollection<DataScheduleRow>(AppConfig.AllRows);
            //    DataRowsView = CollectionViewSource.GetDefaultView(DataRows);
            //    //DataRowsView.Filter = FilterByNames;
            //    //DataRowsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(DataScheduleRow.Subject.Name)));
            //}
        }

        //private bool FilterByNames(object obj)
        //{
        //    if (obj is DataScheduleRow dataRow)
        //    {
        //        return dataRow.Class.Id.Contains(TextFilter) ||
        //            dataRow.Subject.Name.Contains(TextFilter) ||
        //            dataRow.Teacher.Name.Contains(TextFilter);
        //    }
        //    return false;
        //}
    }
}