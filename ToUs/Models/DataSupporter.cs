﻿using System.Collections.Generic;
using System.Linq;

namespace ToUs.Models
{
    public class DataSupporter
    {
        public static List<DataScheduleRow> GetAllDataRows()
        {
            using (var db = new TOUSEntities())
            {
                var query = from manager in db.ClassManagers
                            join classItem in db.Classes on manager.ClassId equals classItem.Id
                            join subject in db.Subjects on manager.SubjectId equals subject.Id
                            join teacher in db.Teachers on manager.TeacherId equals teacher.Id into results
                            from item in results.DefaultIfEmpty()
                            select new { subject, classItem, item };
                return query.ToList().Select(item
                   => new DataScheduleRow(item.subject, item.classItem, item.item)).ToList();
            }
        }
    }

    public class DataScheduleRow
    {
        private Teacher _teacher;
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

        public Teacher Teacher
        {
            get
            {
                if (_teacher != null)
                    return _teacher;
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

        public DataScheduleRow(Subject subject, Class @class, Teacher teacher)
        {
            _teacher = teacher;
            _subject = subject;
            _class = @class;
            _isChecked = false;
        }
    }
}