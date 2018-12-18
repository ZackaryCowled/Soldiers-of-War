using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public Vector3 positionOffset = Vector3.zero;
	public Vector3 rotationOffset = Vector3.zero;
	public float positionDamping = 1.0f;

	void Awake()
	{
		transform.position = target.position + positionOffset;
		transform.rotation = Quaternion.Euler(rotationOffset);
	}

	void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, target.position + positionOffset, positionDamping * Time.deltaTime);
	}
}
