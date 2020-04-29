namespace Import {
	public interface IImport {
		T Read<T>(string data);
	}
}