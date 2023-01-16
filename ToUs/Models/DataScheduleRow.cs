using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUs.Models
{
    public class DataScheduleRow
    {
        private List<Teacher> _teachers;
        private Subject _subject;
        private Class _class;
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
            }
        }

        public List<Teacher> Teacher
        {
            get
            {
                if (_teachers != null)
                    return _teachers;
                return null;
            }
        }

        public Subject Subject
        {
            get
            {
                if (_subject != null)
                    return _subject;
                return null;
            }
        }

        public Class Class
        {
            get
            {
                if (_class != null)
                    return _class;
                return null;
            }
        }

        public DataScheduleRow(Subject subject, Class @class, List<Teacher> teachers)
        {
            _teachers = teachers;
            _subject = subject;
            _class = @class;
            _isChecked = false;
        }
    }
}