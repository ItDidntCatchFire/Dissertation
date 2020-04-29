using System.IO;

namespace Export {
	public interface IExport {
		string ContentType { get; }
		string Extenstion { get; }
		MemoryStream Convert<T>(T obj);
	}
}