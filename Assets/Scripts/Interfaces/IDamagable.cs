using UnityEngine;

public interface IDamagable
{
	float Health { get; }
	float Defence { get; }
	GameObject ImpactPrefab { get; }
}
