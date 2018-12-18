using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour
{
	public new Rigidbody rigidbody;

	void Awake()
	{
		rigidbody.angularDrag = 0.0f;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

		Initialize();
	}

	protected virtual void Initialize() { }

	public void Translate(Vector3 translation)
	{
		rigidbody.MovePosition(rigidbody.position + translation);
	}

	public void SetLookDirection(float x, float z)
	{
		rigidbody.MoveRotation(Quaternion.AngleAxis(Mathf.Atan2(z, x) * Mathf.Rad2Deg - 90.0f, -transform.up));
	}
}
