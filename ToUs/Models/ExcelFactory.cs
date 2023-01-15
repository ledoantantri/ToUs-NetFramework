using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ToUs.Models
{
    public static class ExcelImportDB
    {
        private static DataTableCollection _tableCollection;

        public static bool Connect()
        {
            try
            {
                _tableCollection = ExcelReader.ExcelDataTables;
                return true;
            }
            catch (NoDatasException)
            {
                MessageBox.Show("No data to import to db");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public static List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string id = row["MÃ MH"].ToString();

                    if (!subjects.Any(subject => subject.Id == id))
                    {
                        Subject subject = new Subject()
                        {
                            Id = id,
                            Name = row["TÊN MÔN HỌC"].ToString().Trim(),
                            NumberOfDigits = int.Parse(row["SỐ TC"].ToString().Trim()),
                            HTGD = row["HTGD"].ToString().Trim(),
                            FacultyId = row["KHOA QL"].ToString().Trim(),
                            IsLab = row["THỰC HÀNH"].ToString().Trim() == "1" ? true : false,
                        };
                        subjects.Add(subject);
                    }
                }
            }
            return subjects;
        }

        private static List<Teacher> GetTeachers(DataRow dataRow)
        {
            DataTable dt = dataRow.Table;
            List<Teacher> teachers = null;
            char[] splitChars = { '\n' };
            if (dt.Columns.Contains("MÃ GIẢNG VIÊN") && dt.Columns.Contains("TÊN GIẢNG VIÊN"))
            {
                teachers = new List<Teacher>();
                string[] ids = dataRow["MÃ GIẢNG VIÊN"].ToString().Split(splitChars);
                string[] names = dataRow["TÊN GIẢNG VIÊN"].ToString().Split(splitChars);
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Trim() != "")
                    {
                        Teacher teacher = new Teacher()
                        {
                            Id = ids[i].Trim(),
                            Name = names[i].Trim(),
                            IsContracted = true,
                        };
                        teachers.Add(teacher);
                    }
                }
            }
            return teachers;
        }

        public static List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    List<Teacher> teacherInRows = GetTeachers(row);
                    foreach (var teacher in teacherInRows)
                    {
                        if (!String.IsNullOrEmpty(teacher.Id))
                        {
                            if (!teachers.Any(teacherItem => teacherItem.Id == teacher.Id))
                            {
                                teachers.Add(teacher);
                            }
                        }
                    }
                }
            }

            return teachers;
        }

        public static List<Class> GetAllClasses()
        {
            var classes = new List<Class>();
            //List<Class> classes = new List<Class>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string id = row["MÃ LỚP"].ToString().Trim();
                    int year = int.Parse(row["NĂM HỌC"].ToString().Trim());
                    int semester = int.Parse(row["HỌC KỲ"].ToString().Trim());

                    //if not exitsted ma mh then add to subjects
                    if (!String.IsNullOrEmpty(id))
                    {
                        if (classes.Any(classChecked => classChecked.Id == id &&
                                        classChecked.Year == year &&
                                        classChecked.Semester == semester))
                        {
                            int index = classes.FindIndex(classChecked => classChecked.Id == id &&
                                                                          classChecked.Year == year &&
                                                                          classChecked.Semester == semester);
                            string currentDayInWeek = classes[index].DayInWeek;
                            string currentLession = classes[index].Lession;
                            classes[index].Lession = currentLession + "|" + row["THỨ"].ToString().Trim();
                            classes[index].DayInWeek = currentDayInWeek + "|" + row["TIẾT"].ToString().Trim();
                        }
                        else
                        {
                            Class classToUs = new Class()
                            {
                                Id = id,
                                Semester = semester,
                                Year = year,
                                DayInWeek = row["THỨ"].ToString().Trim(),
                                Room = row["PHÒNG HỌC"].ToString().Trim(),
                                Lession = row["TIẾT"].ToString().Trim(),
                                NumberOfStudents = int.Parse(row["SĨ SỐ"].ToString().Trim()),
                                Frequency = int.Parse(row["CÁCH TUẦN"].ToString().Trim()),
                                Language = row["NGÔN NGỮ"].ToString().Trim(),
                                BeginDate = DateTime.Parse(row["NBD"].ToString().Trim()),
                                EndDate = DateTime.Parse(row["NKT"].ToString().Trim()),
                                Note = row["GHICHU"].ToString().Trim(),
                                System = row["HỆ ĐT"].ToString().Trim()
                            };
                            classes.Add(classToUs);
                        }
                    }
                }
            }
            return classes;
        }

        public static List<Faculty> GetAllFaculty()
        {
            List<Faculty> faculties = new List<Faculty>();
            foreach (DataTable dataTable in _tableCollection)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string id = row["KHOA QL"].ToString();
                    if (!String.IsNullOrEmpty(id))
                    {
                        if (!faculties.Any(faculty => faculty.Id == id))
                        {
                            Faculty faculty = new Faculty()
                            {
                                Id = id,
                                Name = dataTable.Columns.Contains("TÊN KHOA") ? row["TÊN KHOA"].ToString().Trim() : null
                            };
                            faculties.Add(faculty);
                        }
                    }
                }
            }
            return faculties;
        }

        public static List<ClassManager> GetAllClassManager()
        {
            List<ClassManager> classManagers = new List<ClassManager>();
            try
            {
                foreach (DataTable dataTable in _tableCollection)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<Teacher> teacherInRows = GetTeachers(row);

                        var classManager = new ClassManager()
                        {
                            SubjectId = row["MÃ MH"].ToString().Trim(),
                            ClassId = row["MÃ LỚP"].ToString().Trim(),
                            IsDelete = false,
                            Type = ExcelReader.Type,
                            Semester = int.Parse(row["HỌC KỲ"].ToString().Trim()),
                            Year = int.Parse(row["NĂM HỌC"].ToString().Trim()),
                            TeacherId = null
                        };

                        if (teacherInRows.Count > 0)
                        {
                            foreach (var teacher in teacherInRows)
                            {
                                if (!String.IsNullOrEmpty(teacher.Id))
                                    classManager.TeacherId = teacher.Id;
                                classManagers.Add(classManager);
                            }
                        }
                        else
                            classManagers.Add(classManager);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return classManagers;
        }

        private static string[] GetDuplicatePrimaryKeys(string errorMessage)
        {
            string[] ids = null;
            char[] seperatorErrorKeys = { '(', ')' };
            string[] error = errorMessage.Split(seperatorErrorKeys);
            if (error.Length == 3)
            {
                char[] seperatorPrimaryKeys = { ',' };
                ids = error[1].Split(seperatorPrimaryKeys);
                for (int i = 0; i < ids.Length; i++)
                    ids[i] = ids[i].Trim();
            }
            return ids;
        }

        private static string GetDuplicateRecordId(string errorMessage)
        {
            string id = "";
            char[] seperatorChars = { '(', ')' };
            string[] result = errorMessage.Split(seperatorChars);
            if (result.Length == 3)
                id = result[1];
            return id;
        }

        public static object Import<T>(List<T> list, params string[] propertyNameOfPrimaryKes) where T : class
        {
            List<T> duplicateValues = new List<T>();

            string[] duplicatePrimaryKeys;
            do
            {
                duplicatePrimaryKeys = null;
                try
                {
                    if (list.Count > 0)
                    {
                        using (var context = new TOUSEntities())
                        {
                            context.BulkInsert<T>(list);
                            context.BulkSaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    duplicatePrimaryKeys = GetDuplicatePrimaryKeys(e.Message);
                    if (duplicatePrimaryKeys == null || duplicatePrimaryKeys.Length <= 0)
                        MessageBox.Show(e.Message);
                    else
                    {
                        int propertysLength = propertyNameOfPrimaryKes.Length;
                        int primaryKeysLength = duplicatePrimaryKeys.Length;

                        var duplicateRecord = list.FirstOrDefault(item =>
                        {
                            bool isFound = false;
                            Type type = item.GetType();

                            if (propertyNameOfPrimaryKes.Length != duplicatePrimaryKeys.Length)
                            {
                                MessageBox.Show("Bạn truyền dư hoặc thiếu primary key trong lúc import " + type.Name);
                            }
                            else
                            {
                                int count = 0;
                                bool[] isCheckeds = new bool[duplicatePrimaryKeys.Length];
                                for (int i = 0; i < isCheckeds.Length; i++)
                                    isCheckeds[i] = false;

                                for (int i = 0; i < propertysLength; i++)
                                {
                                    string propertyName = propertyNameOfPrimaryKes[i];
                                    string propertyValue = type.GetProperty(propertyName)
                                                                 .GetValue(item).ToString();
                                    for (int j = 0; j < primaryKeysLength; j++)
                                    {
                                        string primaryKey = duplicatePrimaryKeys[j];

                                        if (isCheckeds[j] == false && propertyValue == primaryKey)
                                        {
                                            count++;
                                            isCheckeds[j] = true;
                                            break;
                                        }
                                    }
                                }
                                if (count == propertysLength)
                                    isFound = true;
                            }
                            return isFound;
                        });
                        if (duplicateRecord != null)
                        // Cái này để loại bỏ dữ liệu bị trùng
                        {
                            duplicateValues.Add(duplicateRecord);
                            list.Remove(duplicateRecord);
                        }
                    }
                }
            } while (duplicatePrimaryKeys != null);

            return duplicateValues;
        }

        public static async Task<object> ImportAsync<T>(List<T> list, CancellationToken cancellationToken = default, params string[] propertyNameOfPrimaryKes) where T : class
        {
            List<T> duplicateValues = new List<T>();

            Task task = new Task(async () =>
            {
                string[] duplicatePrimaryKeys;
                do
                {
                    duplicatePrimaryKeys = null;
                    try
                    {
                        if (list.Count > 0)
                        {
                            using (var context = new TOUSEntities())
                            {
                                await context.BulkInsertAsync<T>(list);
                                await context.BulkSaveChangesAsync();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        duplicatePrimaryKeys = GetDuplicatePrimaryKeys(e.Message);
                        if (duplicatePrimaryKeys == null || duplicatePrimaryKeys.Length <= 0)
                            MessageBox.Show(e.Message);
                        else
                        {
                            int propertysLength = propertyNameOfPrimaryKes.Length;
                            int primaryKeysLength = duplicatePrimaryKeys.Length;

                            var duplicateRecord = list.FirstOrDefault(item =>
                            {
                                bool isFound = false;
                                Type type = item.GetType();

                                if (propertyNameOfPrimaryKes.Length != duplicatePrimaryKeys.Length)
                                {
                                    MessageBox.Show("Bạn truyền dư hoặc thiếu primary key trong lúc import " + type.Name);
                                }
                                else
                                {
                                    int count = 0;
                                    bool[] isCheckeds = new bool[duplicatePrimaryKeys.Length];
                                    for (int i = 0; i < isCheckeds.Length; i++)
                                        isCheckeds[i] = false;

                                    for (int i = 0; i < propertysLength; i++)
                                    {
                                        string propertyName = propertyNameOfPrimaryKes[i];
                                        PropertyInfo propertyInfo = type.GetProperty(propertyName);
                                        if (propertyInfo != null)
                                        {
                                            string propertyValue = propertyInfo.GetValue(item).ToString();
                                            for (int j = 0; j < primaryKeysLength; j++)
                                            {
                                                string primaryKey = duplicatePrimaryKeys[j];

                                                if (isCheckeds[j] == false && propertyValue == primaryKey)
                                                {
                                                    count++;
                                                    isCheckeds[j] = true;
                                                    break;
                                                }
                                            }
                                        }
                                        //string propertyValue = type.GetProperty(propertyName)
                                        //                             ?.GetValue(item).ToString();
                                    }
                                    if (count == propertysLength)
                                        isFound = true;
                                }
                                return isFound;
                            });
                            if (duplicateRecord != null)
                            // Cái này để loại bỏ dữ liệu bị trùng
                            {
                                duplicateValues.Add(duplicateRecord);
                                list.Remove(duplicateRecord);
                            }
                        }
                    }
                } while (duplicatePrimaryKeys != null);
            });
            task.Start();
            if (cancellationToken.IsCancellationRequested)
            {
                MessageBox.Show($"{typeof(T).Name} Importing Stop");
                throw new TaskCanceledException(task);
            }
            await task;
            return duplicateValues;
        }

        public static async Task ImportToDBAsync()
        {
            try
            {
                await ImportAsync<Faculty>(GetAllFaculty(), CancellationToken.None, "Id");
                Task<object> subjectTask = ImportAsync<Subject>(GetAllSubjects(), CancellationToken.None, "Id");
                Task<object> teacherTask = ImportAsync<Teacher>(GetAllTeachers(), CancellationToken.None, "Id");
                Task<object> classTask = ImportAsync<Class>(GetAllClasses(), CancellationToken.None, "Id", "Year", "Semester");
                await Task.WhenAll(subjectTask, teacherTask, classTask);
                await ImportAsync<ClassManager>(GetAllClassManager(), CancellationToken.None, "Id");
                MessageBox.Show("File đã được load thành công");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void ImportToDB()
        {
            try
            {
                Import<Faculty>(GetAllFaculty(), "Id");
                Import<Subject>(GetAllSubjects(), "Id");
                Import<Teacher>(GetAllTeachers(), "Id");
                Import<Class>(GetAllClasses(), "Id", "Year", "Semester");
                Import<ClassManager>(GetAllClassManager(), "Id");
                MessageBox.Show("File đã được load thành công");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    public static class ExcelReader
    {
        private static readonly string[] SUBJECT_ID_HEADER_KEYS = { "MÃ MÔN HỌC" };

        private const string STORAGE_RELATIVE_PATH = @".\..\..\Resources\Temps\Excels";
        private const string FORMAT = @".xlsx";
        private const string STORED_FILE_NAME_SUFFIEX = @"_ToUs_";
        private static readonly string[] LANGUAGES = { "EN", "VN", "JP" };
        private static DataTableCollection _dataTableCollection;
        private static string _path = String.Empty;
        private static bool _noInvalidRow = false;
        private static bool _noInvalidColumn = false;
        public static string Type { get; set; }

        public static string FilePath
        {
            get
            {
                if (!File.Exists(_path))
                    return null;
                return _path;
            }
        }

        public static string StoragePath
        {
            get
            {
                return System.IO.Directory.Exists(STORAGE_RELATIVE_PATH) ?
                    Path.GetFullPath(STORAGE_RELATIVE_PATH) :
                    System.IO.Directory.CreateDirectory(STORAGE_RELATIVE_PATH).ToString();
            }
        }

        public static DataTableCollection ExcelDataTables
        {
            get
            {
                if (_dataTableCollection.Count == 0)
                    throw new NoDatasException("No data to get");
                return _dataTableCollection;
            }
        }

        public static bool Open(string path, string type)
        {
            try
            {
                _noInvalidColumn = false;
                _noInvalidRow = false;
                //_path = SaveToSystem(path);
                Type = type;
                _path = SaveToSystem(path);
                //Open system file amd returns it as a stream
                using (FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read))
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        //Get all the Tables
                        _dataTableCollection = result.Tables;
                    }
                }
                //Create move temp file after open
                RemoveTempFile();
                if (!IsTimeManagementExcelFile())
                {
                    throw new NotCorrectFileException();
                }
                return true;
            }
            catch (FileNotFoundException fileNotFound)
            {
                MessageBox.Show(fileNotFound.Message);
                //File.Delete(_path);
                return false;
            }
            catch (NotCorrectFileException notCorrectFile)
            {
                MessageBox.Show(notCorrectFile.Message);
                //File.Delete(_path);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //File.Delete(_path);
                return false;
            }
        }

        private static bool IsTimeManagementExcelFile()
        {
            try
            {
                string[] wordKeys = { "Time", "Management", "THỜI KHÓA BIỂU", "DANH SÁCH LỚP" };
                foreach (DataTable table in _dataTableCollection)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        if (wordKeys.Any(wordKey => column.ColumnName.ToString().ToLower().Contains(wordKey.ToLower())))
                            return true;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            if (wordKeys.Any(wordKey => table.Rows[i][column].ToString().ToLower().Contains(wordKey.ToLower())))
                                return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static bool IsRowInvalid(DataRow row)
        {
            int spaceCount = 0;
            foreach (DataColumn col in row.Table.Columns)
            {
                if (String.IsNullOrEmpty(row[col].ToString()))
                {
                    spaceCount++;
                    if (spaceCount > 7)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Remove invalid rowdata use table index
        /// </summary>
        /// <param name="tableIndex"> </param>
        private static void RemoveInvalidRowData(int tableIndex)
        {
            DataTable dataTable = _dataTableCollection[tableIndex];
            dataTable.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Remove invalid rowdata use table name
        /// </summary>
        /// <param name="tableName"> </param>
        private static void RemoveInvalidRowData(string tableName)
        {
            DataTable dataTable = _dataTableCollection[tableName];
            dataTable.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
            dataTable.AcceptChanges();
        }

        private static void RemoveInvalidRowData(DataTable dataTable)
        {
            if (dataTable == null)
                return;
            dataTable.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Remove all invalid row for all data table
        /// </summary>
        private static void RemoveInvalidRowData()
        {
            List<DataTable> emptyTables = new List<DataTable>();
            if (_noInvalidRow == false)
            {
                foreach (DataTable table in _dataTableCollection)
                {
                    //table.Rows.Cast<DataRow>().Where(IsRowInvalid).ToList().ForEach(r => r.Delete());
                    //table.AcceptChanges();
                    RemoveInvalidRowData(table);
                    _noInvalidRow = true;
                    if (table.Rows.Count < 2)
                        emptyTables.Add(table);
                }
                foreach (var table in emptyTables)
                    _dataTableCollection.Remove(table);
            }
        }

        private static bool IsLangugeColumn(DataColumn dataColumn)
        {
            int rowCount = dataColumn.Table.Rows.Count;
            int numberOfRowChecking = 5 < rowCount ? 5 : rowCount - 1;
            int rowCheckIndex = 1;
            while (numberOfRowChecking > 0)
            {
                bool isLanguage = false;

                string valueChecked = dataColumn.Table.Rows[rowCheckIndex][dataColumn].ToString();
                foreach (string lang in LANGUAGES)
                {
                    if (valueChecked == lang)
                    {
                        isLanguage = true;
                        break;
                    }
                }
                if (!isLanguage)
                    return false;
                rowCheckIndex++;
                numberOfRowChecking--;
            }
            return true;
        }

        private static void ChangeHeaderData(Func<DataColumn> thingToDo, string newName)
        {
            DataColumn dataColumn = thingToDo();
            if (dataColumn != null)
                dataColumn.Table.Rows[0][dataColumn] = newName;
        }

        private static void FormatColumn(DataTable dataTable)
        {
            if (dataTable == null)
                return;
            if (!_noInvalidRow)
                RemoveInvalidRowData(dataTable);
            //Do nothing if no data row

            //format language column
            ChangeHeaderData(() => dataTable.Columns.Cast<DataColumn>().Where(IsLangugeColumn).ToList().FirstOrDefault(), "NGÔN NGỮ");

            //format trợ giảng
            ChangeHeaderData(() => dataTable.Columns.Cast<DataColumn>()
                                                    .Where(c => c.Table.Rows[0][c].ToString().Trim() == "TÊN TRỢ GIẢNG")
                                                    .ToList().FirstOrDefault(), "TÊN GIẢNG VIÊN");
            ChangeHeaderData(() => dataTable.Columns.Cast<DataColumn>()
                                                    .Where(c => c.Table.Rows[0][c].ToString().Trim() == "TỐ TC")
                                                    .ToList().FirstOrDefault(), "SỐ TC");
            List<DataColumn> invalidColumns = dataTable.Columns.Cast<DataColumn>()
                                                        .Where(c => String.IsNullOrEmpty(c.Table.Rows[0][c].ToString().Trim()) || c.Table.Rows[0][c].ToString().Trim() == "Đã ĐK")
                                                        .ToList();
            foreach (DataColumn column in invalidColumns)
                dataTable.Columns.Remove(column);
            dataTable.AcceptChanges();
        }

        private static void FormatColumn()
        {
            foreach (DataTable dataTable in _dataTableCollection)
            {
                FormatColumn(dataTable);
            }
        }

        /// <summary>
        /// Set the column name at one data table
        /// </summary>
        /// <param name="dataTable"> </param>
        private static void SetColumnName(DataTable dataTable)
        {
            if (dataTable == null)
                return;
            if (!_noInvalidRow)
                RemoveInvalidRowData(dataTable);
            if (!_noInvalidColumn)
                FormatColumn(dataTable);
            //Do nothing if no data row
            try
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string newName = dataTable.Rows[0][i].ToString();
                    if (!String.IsNullOrEmpty(newName))
                        dataTable.Columns[i].ColumnName = newName;
                }
                dataTable.Rows[0].Delete();

                dataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Set column name for all table
        /// </summary>
        private static void SetColumnName()
        {
            foreach (DataTable dataTable in _dataTableCollection)
            {
                SetColumnName(dataTable);
            }
        }

        public static void FormatExcelDatas()
        {
            RemoveInvalidRowData();
            FormatColumn();
            SetColumnName();
        }

        public static int GetLastSystemNumFile()
        {
            int lastNum = 0;
            foreach (string path in Directory.GetFiles(StoragePath))
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                string[] splitCharators = { STORED_FILE_NAME_SUFFIEX };
                string[] result = fileName.Split(splitCharators, StringSplitOptions.RemoveEmptyEntries);

                if (result.Length == 2)
                {
                    string suffix = result[1];
                    int num = int.Parse(suffix);
                    if (num > lastNum)
                        lastNum = num;
                }
            }
            return lastNum;
        }

        /// <summary>
        /// Create a path to save to the excel storage follow ToUs rule
        /// </summary>
        /// <returns> </returns>
        public static string GenerateSystemPath(string oldPath)
        {
            int nextNum = GetLastSystemNumFile() + 1;
            string oldFileName = Path.GetFileNameWithoutExtension(oldPath);
            string newFileName = oldFileName + STORED_FILE_NAME_SUFFIEX + nextNum.ToString() + FORMAT;
            return Path.Combine(StoragePath, newFileName);
        }

        public static string GenerateSystemPath(string oldPath, string adminReposPath)
        {
            int nextNum = GetLastSystemNumFile() + 1;
            string oldFileName = Path.GetFileNameWithoutExtension(oldPath);
            string newFileName = oldFileName + adminReposPath + nextNum.ToString() + FORMAT;
            return Path.Combine(StoragePath, newFileName);
        }

        /// <summary>
        /// Save a copy of excel file to excel storage with the ToUs rule
        /// </summary>
        public static string SaveToSystem(string oldPath)
        {
            try
            {
                string newPath = GenerateSystemPath(oldPath);
                File.Copy(oldPath, newPath);
                return newPath;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static string SaveToSystem(string oldPath, string adminReposPath)
        {
            try
            {
                string newPath = GenerateSystemPath(oldPath, adminReposPath);
                File.Copy(oldPath, newPath);
                return newPath;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static bool RemoveTempFile()
        {
            try
            {
                foreach (string path in Directory.GetFiles(StoragePath))
                {
                    File.Delete(path);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}