using System.Collections.Generic;
using ToUs.View.HomePageView;

namespace ToUs.Models
{
    public static partial class AppConfig
    {
        private static User _user;
        private static User _userDetail;
        private static string _connectionString;
        private static List<DataScheduleRow> _selectedRows = new List<DataScheduleRow>();
        private static List<DataScheduleRow> _allRows = new List<DataScheduleRow>();

        public static User User
        {
            get
            {
                if (_user != null)
                    return _user;
                return null;
            }
            set
            {
                _user = value;
            }
        }

        public static User UserDetail
        {
            get
            {
                if (_userDetail != null)
                    return _userDetail;
                return null;
            }
            set
            {
                _userDetail = value;
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString != null)
                    return _connectionString;
                return null;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static List<DataScheduleRow> SelectedRows
        {
            get
            {
                if (_selectedRows != null)
                    return _selectedRows;
                return null;
            }
            set
            {
                _selectedRows = value;
            }
        }

        public static List<DataScheduleRow> AllRows
        {
            get
            {
                if (_allRows != null)
                    return _allRows;
                return null;
            }
            set
            {
                _allRows = value;
            }
        }
    }
}