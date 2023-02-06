﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToUs.ViewModel.StartViewModel.ComponentAuthenticateViewModel;

namespace ToUs.Models
{
    public static class AppConfiguration
    {
        private static Random _rand = new Random();
        private static string _codeSent;
        private static string _userEmail;
        private static UserDetail _userDetail;
        private static string connectionString;
        private static List<DataScheduleRow> _selectedRows = new List<DataScheduleRow>();
        private static List<DataScheduleRow> _allRows = new List<DataScheduleRow>();

        private static string _currentExcelPath = null;


        public static Random Rand
        {
            get { return _rand; }
            set { _rand = value; }
        }

        public static string CodeSent
        {
            get { return _codeSent; }
            set { _codeSent = value; }
        }

        public static string CurrentExcelPath
        {
            get
            {
                return _currentExcelPath;
            }
            set { _currentExcelPath = value; }
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

        public static string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        public static UserDetail UserDetail
        {
            get { return _userDetail; }
            set { _userDetail = value; }
        }


        public static string ConnectionString
        {
            get
            {
                if (connectionString != null)
                    return connectionString;
                return null;
            }
            set
            {
                connectionString = value;
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

        public static class TempSignUpDetail
        {
            public static string FirstName;
            public static string LastName;
            public static string Email;
            public static string Password;
            public static string ConfirmPassword;

            public static void DeleteTempDetail()
            {
                FirstName = LastName = Email = Password = ConfirmPassword = null;
            }
        }

    }
}