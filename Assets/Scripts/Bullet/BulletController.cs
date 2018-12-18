using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class BulletController : PoolObjectBehaviour
{
	public new Rigidbody rigidbody;
	public TrailRenderer trailRenderer;
	public GameObject impactPrefab;
	public float bulletDamage = 15.0f;

	HealthController hitHealthController;

	public void Fire(float force, Vector3 position, Vector3 direction)
	{
		trailRenderer.Clear();
		trailRenderer.AddPosition(position);

		rigidbody.MovePosition(position);
		rigidbody.MoveRotation(Quaternion.LookRotation(direction, Vector3.up));

		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
	}

	void OnCollisionEnter(Collision collision)
	{
		hitHealthController = collision.gameObject.GetComponent<HealthController>();

		if (hitHealthController != null)
		{
			BulletImpactPool.CreateInstance(hitHealthController.ImpactPrefab).Initialize(collision.contacts[0].point, collision.contacts[0].normal, collision.contacts[0].otherCollider.transform);
			hitHealthController.Damage(bulletDamage);
		}

		Pool.DestroyObject(PoolObjectID);
	}

	void OnCollisionStay(Collision collision)
	{
		Debug.Log("Stay");
		hitHealthController = collision.gameObject.GetComponent<HealthController>();

		if (hitHealthController != null)
		{
			BulletImpactPool.CreateInstance(hitHealthController.ImpactPrefab).Initialize(collision.contacts[0].point, collision.contacts[0].normal, collision.contacts[0].otherCollider.transform);
			hitHealthController.Damage(bulletDamage);
		}

		Pool.DestroyObject(PoolObjectID);
	}
}
