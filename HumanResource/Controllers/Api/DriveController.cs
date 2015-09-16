using System;
using System.Web.Http;
using HumanResource.Models.Drives;

namespace HumanResource.Controllers.Api
{
    public class DriveController : ApiController
    {
        #region Private

        private IFixedDriveRepository _fixedDrive = FixedDriveRepository.Current;

        #endregion

        #region API Methods

        [Route("api/drive/directory")]
        [HttpGet]
        public FixedDrive Directory(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return new FixedDrive
                {
                    Folders = _fixedDrive.Drives,
                    ParentPath = _fixedDrive.GetParentPath(path)
                };
            }
            else
            {
                return new FixedDrive
                {
                    Folders = _fixedDrive.GetFoldersNames(path),
                    Files = _fixedDrive.GetFilesNames(path),
                    CurrentPath = path,
                    ParentPath =  _fixedDrive.GetParentPath(path)
                };
            }
        }
        
        [Route("api/drive/countFiles")]
        [HttpGet]
        public FixedDrive CountFiles(string path)
        {
            _fixedDrive.CalculateFiles(path);
            return new FixedDrive
            {
                CountFilesLessThat_10mb = _fixedDrive.CountFilesLessThat_10mb,
                CountFilesMoreThat_100mb = _fixedDrive.CountFilesMoreThat_100mb,
                CountFiles_10_50mb = _fixedDrive.CountFilesBetween_10_50mb
            };
        }

        #endregion
    }
}
