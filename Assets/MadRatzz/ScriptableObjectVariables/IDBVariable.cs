public interface IDBVariable
{
	bool SyncEnabled { get; }
	void Update(object value);
	object GetValue();
}