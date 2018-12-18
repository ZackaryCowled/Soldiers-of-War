namespace UnityProductive
{
	public interface IPoolObject
	{
		int PoolObjectID { get; set; }

		Pool Pool { get; set; }

		void Destroy();

		void Recycle();
	}
}
