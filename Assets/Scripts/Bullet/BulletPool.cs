using UnityEngine;
using UnityProductive;

public static class BulletPool
{
	static Pool bulletPool = new Pool();
	static BulletFactory bulletFactory = new BulletFactory();

	public static BulletController CreateInstance(GameObject bulletPrefab)
	{
		return bulletPool.CreateObject<BulletController, BulletFactory>(bulletFactory, bulletPrefab);
	}
}
