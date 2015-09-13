using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HumanResource.Models.Drives;

namespace HumanResource.Controllers.Api
{
    public class DriveController : ApiController
    {
        private IFixedDriveRepository _fixedDrive = FixedDriveRepository.Current;

        public IEnumerable<string> GetDrives()
        {
            return _fixedDrive.Drives;
        }
        
        public FixedDrive GetDirectory(string path)
        {
            return new FixedDrive
            {
                Folders = _fixedDrive.GetFoldersNames(path),
                Files = _fixedDrive.GetFilesNames(path),
                CurrentPath = path
            };
        }

        public FixedDrive GetCountFiles(FixedDrive fixedDrive)
        {
            _fixedDrive.CalculateFiles(fixedDrive.CurrentPath);
            return new FixedDrive
            {
                CountFilesLessThat_10mb = _fixedDrive.CountFilesLessThat_10mb,
                CountFilesMoreThat_100mb = _fixedDrive.CountFilesMoreThat_100mb,
                CountFiles_10_50mb = _fixedDrive.CountFilesBetween_10_50mb
            };
        }
        
    }
}
