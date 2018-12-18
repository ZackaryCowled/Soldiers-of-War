using UnityEngine;
using UnityProductive;

public class BulletImpactFactory : IFactory<BulletImpactController>
{
	public BulletImpactController CreateInstance(params object[] args)
	{
		return Object.Instantiate((GameObject)args[0]).GetComponent<BulletImpactController>();
	}
}
