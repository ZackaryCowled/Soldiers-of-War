using UnityEngine;

public class HealthController : MonoBehaviour, IDamagable
{
	public delegate void DamageEvent();
	public event DamageEvent OnDamage;

	public delegate void DeathEvent();
	public event DeathEvent OnDeath;

	public float Health
	{
		get { return health; }
		set { health = value; }
	}

	public float Defence
	{
		get { return defence; }
		set { defence = value; }
	}

	public GameObject ImpactPrefab
	{
		get { return impactPrefab; }
		set { impactPrefab = value; }
	}

	[SerializeField]
	float health = 100.0f;

	[SerializeField]
	float defence = 10.0f;

	[SerializeField]
	GameObject impactPrefab;

	public void Damage(float attack)
	{
		if (attack > Defence)
		{
			health -= attack - defence;

			if (health <= 0.0f)
			{
				OnDeath?.Invoke();
			}
			else
			{
				OnDamage?.Invoke();
			}
		}
	}
}
