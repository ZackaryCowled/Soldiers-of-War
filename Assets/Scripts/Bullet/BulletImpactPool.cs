using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

public static class BulletImpactPool
{
	static Dictionary<GameObject, Pool> bulletImpactPoolMap = new Dictionary<GameObject, Pool>();
	static BulletImpactFactory bulletImpactFactory = new BulletImpactFactory();
	static Pool bulletImpactPool;

	public static BulletImpactController CreateInstance(GameObject bulletImpactPrefab)
	{
		if (!bulletImpactPoolMap.TryGetValue(bulletImpactPrefab, out bulletImpactPool))
		{
			bulletImpactPool = new Pool();
			bulletImpactPoolMap[bulletImpactPrefab] = new Pool();
		}

		return bulletImpactPool.CreateObject<BulletImpactController, BulletImpactFactory>(bulletImpactFactory, bulletImpactPrefab);
	}
}
