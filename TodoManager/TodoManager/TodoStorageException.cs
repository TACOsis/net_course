namespace TodoManager;

public class TodoStorageException : Exception
{
    public TodoStorageException(string message, string? backupPath = null) : base(message)
        => BackupPath = backupPath;

    public string? BackupPath { get; }
}