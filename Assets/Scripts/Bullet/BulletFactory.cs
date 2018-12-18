using UnityEngine;
using UnityProductive;

public class BulletFactory : IFactory<BulletController>
{
	public BulletController CreateInstance(params object[] args)
	{
		return Object.Instantiate((GameObject)args[0]).GetComponent<BulletController>();
	}
}
