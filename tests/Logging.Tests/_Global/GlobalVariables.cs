using BindOpen.Data.Helpers;

namespace BindOpen.Logging.Tests;

public static class GlobalVariables
{
    static string _workingFolder = null;

    /// <summary>
    /// The global working folder.
    /// </summary>
    public static string WorkingFolder
    {
        get
        {
            if (_workingFolder == null)
            {
                _workingFolder = (FileHelper.GetAppRootFolderPath() + @"temp\").ToPath();
            }

            return _workingFolder;
        }
    }
}
