using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace ToUs.Models
{
    public class DataQuery
    {
        public static List<DataScheduleRow> GetAllDataRows(int year, int semester)
        {
            var rows = new List<DataScheduleRow>();

            using (var db = new TOUSEntities())
            {
                var query = (from manager in db.ClassManagers
                             join classItem in db.Classes on manager.ClassId equals classItem.Id
                             join subject in db.Subjects on manager.SubjectId equals subject.Id
                             join teacher in db.Teachers on manager.TeacherId equals teacher.Id into results
                             from item in results.DefaultIfEmpty()
                             where classItem.Year == year && classItem.Semester == semester
                             select new
                             {
                                 Subject = subject,
                                 Class = classItem,
                                 Teacher = item
                             }).ToList();

                foreach (var item in query)
                {
                    int index;
                    if (item.Teacher != null)
                        if (-1 != (index = rows.FindIndex(itemChecked
                            => itemChecked.Class.ClassId == item.Class.ClassId)))
                        {
                            rows[index].Teacher.Add(item.Teacher);
                        }
                        else
                        {
                            var dataRow = new DataScheduleRow
                            {
                                Subject = item.Subject,
                                Class = item.Class,
                                Teacher = new List<Teacher>() { item.Teacher }
                            };
                        }
                }
                return rows;
            }
        }

        public static async Task<List<DataScheduleRow>> GetAllDataRowsAsync(int year, int semester)
        {
            var rows = new List<DataScheduleRow>();

            Task task = new Task(() =>
            {
                using (var db = new TOUSEntities())
                {
                    var query = (from manager in db.ClassManagers
                                 join classItem in db.Classes on manager.ClassId equals classItem.Id
                                 join subject in db.Subjects on manager.SubjectId equals subject.Id
                                 join teacher in db.Teachers on manager.TeacherId equals teacher.Id into results
                                 from item in results.DefaultIfEmpty()
                                 where classItem.Year == year && classItem.Semester == semester
                                 select new
                                 {
                                     Subject = subject,
                                     Class = classItem,
                                     Teacher = item
                                 }).ToList();

                    foreach (var item in query)
                    {
                        int index;
                        if (-1 != (index = rows.FindIndex(itemChecked => itemChecked.Class.ClassId == item.Class.ClassId)))
                        {
                            rows[index].Teacher.Add(item.Teacher);
                        }
                        else
                        {
                            var dataRow = new DataScheduleRow
                            {
                                Subject = item.Subject,
                                Class = item.Class,
                                Teacher = new List<Teacher>() { item.Teacher }
                            };
                        }
                    }
                }
            });
            task.Start();
            await task;
            return rows;
        }

        public static void CreateTimetable(string name, int ownerId, string picturePreviewPath = null)
        {
            using (var context = new TOUSEntities())
            {
                var timeTable = new TimeTable()
                {
                    Name = name,
                    UserDetailId = ownerId,
                    PicturePath = picturePreviewPath,
                };
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
            }
        }

        public static void SaveTheTimeTable(List<DataScheduleRow> classChoosed, int timeTableId)
        {
            using (var context = new TOUSEntities())
            {
                var timeTable = new TimeTable();

                foreach (var item in classChoosed)
                {
                    if (item.Teacher != null)
                    {
                        if (item.Teacher.Count > 0)
                        {
                        }
                    }
                }
            }
        }
    }
}