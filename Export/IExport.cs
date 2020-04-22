using System.IO;

namespace Export
{
    public enum ExportEnum
    {
        JSON,
        XML
    }
    
    public interface IExport
    {
        string ContentType { get; }
        string Extenstion { get; }
        MemoryStream Write<T>(T obj);
    }
}