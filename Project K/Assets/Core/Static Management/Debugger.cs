using UnityEditor;

public class Debugger
{
    
    [MenuItem("CustomDebugger/DeleteFile")]
    private static void Delete() { FileManager.DeleteFile(); }

    [MenuItem("CustomDebugger/WriteFile")]
    private static void Write() { FileManager.WriteFile(); }

    [MenuItem("CustomDebugger/ReadFile")]
    private static void Read() { FileManager.ReadFile(); }
}