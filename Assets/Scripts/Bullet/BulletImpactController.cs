using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

[RequireComponent(typeof(AudioSource))]
public class BulletImpactController : PoolObjectBehaviour
{
	public AudioSource audioSource;
	public SoundEffect bulletImpactSoundEffect;
	public new ParticleSystem particleSystem;
	public float destroyAfterSeconds = 5.0f;

	float timer;

	public void Initialize(Vector3 position, Vector3 direction, Transform parent)
	{
		timer = 0.0f;

		transform.position = position;
		transform.forward = direction;
		transform.SetParent(parent, true);

		audioSource.PlayOneShot(bulletImpactSoundEffect.audioClip, bulletImpactSoundEffect.volume);
		particleSystem.Play(true);
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= destroyAfterSeconds)
		{
			Pool.DestroyObject(PoolObjectID);
		}
	}
}
