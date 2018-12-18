using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : Soldier
{
	public NavMeshAgent navMeshAgent;
	public GameObject target;
	public float minTargetDistance = 3.0f;
	public float maxTargetDistance = 20.0f;
	public float rotationDamping = 2.0f;
	public float shootThreshold = 0.9f;

	Vector3 targetDelta;
	float targetDistance;

	protected override void Initialize()
	{
		base.Initialize();

		navMeshAgent.isStopped = true;
		navMeshAgent.destination = target.transform.position;
	}

	void FixedUpdate()
	{
		if(healthController.Health <= 0.0f)
		{
			return;
		}

		navMeshAgent.destination = target.transform.position;

		targetDistance = Vector3.Distance(navMeshAgent.destination, transform.position);

		if (targetDistance > minTargetDistance)
		{
			if (navMeshAgent.path.corners.Length > 1)
			{
				isRunning = true;
				Translate(Vector3.Normalize(navMeshAgent.path.corners[1] - transform.position) * maxMoveSpeed * Time.fixedDeltaTime);
			}
			else
			{
				isRunning = false;
			}
		}
		else
		{
			isRunning = false;
		}

		if (targetDistance <= maxTargetDistance)
		{
			if (Physics.Linecast(rigidbody.position, target.transform.position))
			{
				isAiming = true;
				targetDelta = Vector3.Normalize(target.transform.position - rigidbody.position);
				SetLookDirection(Mathf.Lerp(transform.forward.x, targetDelta.x, rotationDamping * Time.deltaTime), Mathf.Lerp(transform.forward.z, targetDelta.z, rotationDamping * Time.deltaTime));

				if(Mathf.Abs(Vector3.Dot(transform.forward, target.transform.forward)) >= shootThreshold)
				{ 
					isShooting = true;
				}
				else
				{
					isShooting = false;
				}
			}
			else
			{
				isAiming = false;
				isShooting = false;
			}
		}

		runAnimationSpeed = isRunning ? Mathf.Lerp(runAnimationSpeed, 1.0f, rotationDamping * Time.deltaTime) : Mathf.Lerp(runAnimationSpeed, 0.0f, rotationDamping * Time.deltaTime);

		UpdateAnimator();
	}
}
