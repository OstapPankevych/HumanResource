using System.Collections.Generic;

namespace HumanResource.Models.Drives
{
    public interface IFixedDriveRepository
    {
        #region Properties

        List<string> Drives { get; }
        int CountFilesLessThat_10mb { get; }
        int CountFilesBetween_10_50mb { get; }
        int CountFilesMoreThat_100mb { get; }

        #endregion Prorepties

        #region Methods

        List<string> GetFilesNames(string path);
        List<string> GetFoldersNames(string path);
        string GetParentPath(string path);
        void CalculateFiles(string path);

        #endregion Methods
    }
}
