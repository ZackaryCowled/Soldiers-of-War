using UnityEngine;

public class PlayerController : Soldier
{
	public FixedJoystick leftJoystick;
	public FixedJoystick rightJoystick;

	Vector3 moveDirection;
	Vector2 lookDirection;

	float runMagnitude = 0.0f;

	void Update()
	{
		if (healthController.Health <= 0)
		{
			return;
		}

		moveDirection.x = leftJoystick.Horizontal;
		moveDirection.z = leftJoystick.Vertical;
		runMagnitude = moveDirection.magnitude;

		lookDirection.x = rightJoystick.Horizontal;
		lookDirection.y = rightJoystick.Vertical;

		isRunning = moveDirection.x != 0 || moveDirection.y != 0;
		isShooting = lookDirection.x != 0 || lookDirection.y != 0;
		isAiming = isRunning || isShooting;

		runAnimationSpeed = isRunning ? Mathf.Lerp(minRunAnimationSpeed, maxRunAnimationSpeed, moveDirection.magnitude) : 0.0f;

		UpdateAnimator();
	}

	void FixedUpdate()
	{
		if (healthController.Health <= 0)
		{
			return;
		}

		if (isRunning)
		{
			Translate(moveDirection.normalized * Mathf.Lerp(minMoveSpeed, maxMoveSpeed, runMagnitude) * Time.fixedDeltaTime);

			if (!isShooting)
			{
				SetLookDirection(moveDirection.x, moveDirection.z);
			}
		}

		if (isShooting)
		{
			SetLookDirection(lookDirection.x, lookDirection.y);
		}
	}
}
