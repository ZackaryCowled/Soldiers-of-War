using UnityEngine;

namespace UnityProductive
{
	public class PoolObjectBehaviour : MonoBehaviour, IPoolObject
	{
		public int PoolObjectID { get; set; }

		public Pool Pool { get; set; }

		public void Destroy()
		{
			gameObject.SetActive(false);
		}

		public void Recycle()
		{
			gameObject.SetActive(true);
		}
	}
}
