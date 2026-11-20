namespace BindOpen.Hosting.Settings
{
    public interface IBdoHostKernelSettings
    {
        string ApplicationInstanceName { get; set; }

        string LibraryFolderPath { get; set; }

        int LoggingExpirationDayNumber { get; set; }

        string LoggingFileName { get; set; }

        string LoggingFolderPath { get; set; }
    }
}