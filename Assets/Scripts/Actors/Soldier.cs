using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthController))]
public class Soldier : Actor
{
	public Animator animator;
	public AudioSource audioSource;
	public HealthController healthController;
	public Ragdoll ragdoll;
	public SoundEffect gruntSoundEffect;
	public SoundEffect footstepSoundEffect;
	public SoundEffect gunshotSoundEffect;
	public ParticleSystem muzzleFlash;
	public Transform bulletSpawnpoint;
	public GameObject bulletPrefab;
	public float maxMoveSpeed = 5.0f;
	public float minMoveSpeed = 2.5f;
	public float maxRunAnimationSpeed = 1.25f;
	public float minRunAnimationSpeed = 0.75f;
	public float fireRate = 2.0f;

	protected Vector3 spawnPosition;
	protected Quaternion spawnRotation;

	protected float runAnimationSpeed = 0.0f;
	protected bool isRunning = false;
	protected bool isShooting = false;
	protected bool isAiming = false;

	protected override void Initialize()
	{
		base.Initialize();

		spawnPosition = rigidbody.position;
		spawnRotation = rigidbody.rotation;

		healthController.OnDamage += OnDamage;
		healthController.OnDeath += OnDeath;
	}

	protected virtual void OnDeath()
	{
		ragdoll.Enable();
		animator.enabled = false;
		Invoke("Respawn", 5.0f);
	}

	void Respawn()
	{
		healthController.Health = 100.0f;
		ragdoll.Disable();
		rigidbody.MovePosition(spawnPosition);
		rigidbody.MoveRotation(spawnRotation);
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	protected virtual void OnDamage()
	{
		audioSource.PlayOneShot(gruntSoundEffect.audioClip, gruntSoundEffect.volume);
	}

	public void FireGun()
	{
		PlayGunshot();
		MuzzleFlash();
		SpawnBullet();
	}

	protected void UpdateAnimator()
	{
		animator.SetFloat("FireRate", fireRate);
		animator.SetFloat("RunSpeed", runAnimationSpeed);
		animator.SetBool("IsRunning", isRunning);
		animator.SetBool("IsShooting", isShooting);
		animator.SetBool("IsAiming", isAiming);
	}

	void PlayGunshot()
	{
		audioSource.PlayOneShot(gunshotSoundEffect.audioClip, gunshotSoundEffect.volume);
	}

	void PlayFootstep()
	{
		audioSource.PlayOneShot(footstepSoundEffect.audioClip, footstepSoundEffect.volume);
	}

	void MuzzleFlash()
	{
		muzzleFlash.Emit(1);
	}

	void SpawnBullet()
	{
		BulletPool.CreateInstance(bulletPrefab).Fire(50.0f, bulletSpawnpoint.position, transform.forward);
	}

	void OnCollisionEnter(Collision collision)
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	void OnCollisionStay(Collision collision)
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	void OnCollisionExit(Collision collision)
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
}
