using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
	public Animator animator;
	public List<Rigidbody> rigidbodies = new List<Rigidbody>();
	public List<Collider> colliders = new List<Collider>();
	public List<Rigidbody> inverseRigidbodies = new List<Rigidbody>();
	public List<Collider> inverseColliders = new List<Collider>();

	public void Enable()
	{
		animator.enabled = false;

		foreach (Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = false;
		}

		foreach (Rigidbody rigidbody in inverseRigidbodies)
		{
			rigidbody.isKinematic = true;
		}

		foreach (Collider collider in colliders)
		{
			collider.enabled = true;
		}

		foreach (Collider collider in inverseColliders)
		{
			collider.enabled = false;
		}
	}

	public void Disable()
	{
		animator.enabled = true;

		foreach (Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = true;
		}

		foreach (Rigidbody rigidbody in inverseRigidbodies)
		{
			rigidbody.isKinematic = false;
		}

		foreach (Collider collider in colliders)
		{
			collider.enabled = false;
		}

		foreach (Collider collider in inverseColliders)
		{
			collider.enabled = true;
		}
	}
}
