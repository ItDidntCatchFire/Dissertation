using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Export {
	public class XMLExport : IExport {
		public string ContentType => "text/xml";
		public string Extenstion => ".xml";
		public MemoryStream Convert<T>(T obj) {
			var s = new XmlSerializer(typeof(T));
			var sr = new System.IO.StringWriter();
			s.Serialize(sr, obj);

			var buffer = Encoding.UTF8.GetBytes(sr.ToString());
			return new MemoryStream(buffer);
		}
	}
}