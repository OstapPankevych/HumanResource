using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace HumanResource.Models.Drives
{
    public class FixedDriveRepository : IFixedDriveRepository
    {
        #region Private

        private List<string> _drives = new List<string>();
        private const int MB_10 = 10485760;
        private const int MB_50 = 52428800;
        private const int MB_100 = 1048576000;
        private static IFixedDriveRepository _fixedDrive = new FixedDriveRepository();
        #endregion

        #region Static Fields

        public static IFixedDriveRepository Current
        {
            get { return _fixedDrive; }
        }

        #endregion

        #region Constructor

        private FixedDriveRepository()
        {
            foreach (var x in DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed))
            {
                _drives.Add(x.Name);
            }
        }

        #endregion

        #region Fileds

        public List<string> Drives
        {
            get
            {
                return _drives;
            }
        }

        public int CountFilesLessThat_10mb
        {
            get;
            private set;
        }

        public int CountFilesBetween_10_50mb
        {
            get;
            private set;
        }

        public int CountFilesMoreThat_100mb
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public List<string> GetFoldersNames(string path)
        {
            List<string> directoryList = new List<string>();
            foreach (var x in Directory.GetDirectories(path))
            {
                directoryList.Add(new DirectoryInfo(x).Name);
            }

            return directoryList;
        }

        public List<string> GetFilesNames(string path)
        {
            List<string> fileList = new List<string>();
            foreach (var x in Directory.GetFiles(path))
            {
                fileList.Add(new FileInfo(x).Name);
            }

            return fileList;
        }

        public void CalculateFiles(string path)
        {
            string[] subdirectoryEntries;
            try
            {
                subdirectoryEntries = Directory.GetDirectories(path);
            }
            catch 
            {
                return;
            }
            foreach (string subdirectory in subdirectoryEntries)
            {
                foreach (var fileInfo in Directory.GetFiles(path))
                {
                    IncrementCountFiles(new FileInfo(fileInfo));
                }
                CalculateFiles(subdirectory);
            }
        }

        #endregion

        #region Helpers

        private void IncrementCountFiles(FileInfo fileInfo)
        {
            long fileSize = fileInfo.Length;

            if (fileSize <= MB_10)
            {
                CountFilesLessThat_10mb++;
                return;
            }
            else if (fileSize <= MB_50)
            {
                CountFilesBetween_10_50mb++;
                return;
            }
            else if (fileSize >= MB_100)
            {
                CountFilesMoreThat_100mb++;
            }
        }

        #endregion
    }
}
