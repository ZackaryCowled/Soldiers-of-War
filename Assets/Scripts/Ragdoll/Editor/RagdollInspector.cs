using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Ragdoll))]
public class RagdollInspector : Editor
{
	bool modifiedData = false;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Ragdoll ragdoll = (Ragdoll)target;
		modifiedData = false;

		if (GUILayout.Button("Auto Link Recursive"))
		{
			AutoLinkRecursive(ragdoll);
		}

		if (GUILayout.Button("Disable All"))
		{
			DisableAll(ragdoll);
		}

		if (GUILayout.Button("Enable All"))
		{
			EnableAll(ragdoll);
		}

		if (modifiedData)
		{
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}

	void AutoLinkRecursive(Ragdoll ragdoll)
	{
		if (LinkRigidbody(ragdoll.GetComponent<Rigidbody>(), ragdoll))
		{
			modifiedData = true;
		}

		if (LinkCollider(ragdoll.GetComponent<Collider>(), ragdoll))
		{
			modifiedData = true;
		}

		Rigidbody[] rigidbodies = ragdoll.GetComponentsInChildren<Rigidbody>();
		Collider[] colliders = ragdoll.GetComponentsInChildren<Collider>();

		if (rigidbodies != null && rigidbodies.Length > 0)
		{
			foreach (Rigidbody rigidbody in rigidbodies)
			{
				LinkRigidbody(rigidbody, ragdoll);
			}

			modifiedData = true;
		}

		if (colliders != null && colliders.Length > 0)
		{
			foreach (Collider collider in colliders)
			{
				LinkCollider(collider, ragdoll);
			}

			modifiedData = true;
		}
	}

	bool LinkRigidbody(Rigidbody rigidbody, Ragdoll ragdoll)
	{
		if (rigidbody != null && !ragdoll.rigidbodies.Contains(rigidbody))
		{
			ragdoll.rigidbodies.Add(rigidbody);
			return true;
		}

		return false;
	}

	bool LinkCollider(Collider collider, Ragdoll ragdoll)
	{
		if (collider != null && !ragdoll.colliders.Contains(collider))
		{
			ragdoll.colliders.Add(collider);
			return true;
		}

		return false;
	}

	void DisableAll(Ragdoll ragdoll)
	{
		foreach (Rigidbody rigidbody in ragdoll.rigidbodies)
		{
			if (DisableRigidbody(rigidbody))
			{
				modifiedData = true;
			}
		}

		foreach (Rigidbody rigidbody in ragdoll.inverseRigidbodies)
		{
			if (EnableRigidbody(rigidbody))
			{
				modifiedData = true;
			}
		}

		foreach (Collider collider in ragdoll.colliders)
		{
			if (DisableCollider(collider))
			{
				modifiedData = true;
			}
		}

		foreach (Collider collider in ragdoll.inverseColliders)
		{
			if (EnableCollider(collider))
			{
				modifiedData = true;
			}
		}
	}

	bool DisableRigidbody(Rigidbody rigidbody)
	{
		if (rigidbody != null && rigidbody.isKinematic != true)
		{
			rigidbody.isKinematic = true;
			return true;
		}

		return false;
	}

	bool DisableCollider(Collider collider)
	{
		if (collider.enabled)
		{
			collider.enabled = false;
			return true;
		}

		return false;
	}

	void EnableAll(Ragdoll ragdoll)
	{
		foreach (Rigidbody rigidbody in ragdoll.rigidbodies)
		{
			if (EnableRigidbody(rigidbody))
			{
				modifiedData = true;
			}
		}

		foreach (Rigidbody rigidbody in ragdoll.inverseRigidbodies)
		{
			if (DisableRigidbody(rigidbody))
			{
				modifiedData = true;
			}
		}

		foreach (Collider collider in ragdoll.colliders)
		{
			if (EnableCollider(collider))
			{
				modifiedData = true;
			}
		}

		foreach (Collider collider in ragdoll.inverseColliders)
		{
			if (DisableCollider(collider))
			{
				modifiedData = true;
			}
		}
	}

	bool EnableRigidbody(Rigidbody rigidbody)
	{
		if (rigidbody != null && rigidbody.isKinematic != false)
		{
			rigidbody.isKinematic = false;
			return true;
		}

		return false;
	}

	bool EnableCollider(Collider collider)
	{
		if (!collider.enabled)
		{
			collider.enabled = true;
			return true;
		}

		return false;
	}
}
