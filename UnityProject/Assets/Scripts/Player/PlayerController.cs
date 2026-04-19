using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private GameObject body;

	[SerializeField]
	private GameObject antenna;

	[SerializeField]
	private GameObject pewpew;

	[SerializeField] private PlayerDamagable playerDamagable;

	//GameData SO 
	[SerializeField]
	private SO_Float so_Speed;

	[SerializeField]
	private SO_bool so_controlDevice; //false = keyboard/mouse ; true = controller

	[SerializeField]
	private SO_Float so_Angle; // Angle to face mouse raycast position on a plane

	[SerializeField] private SO_ProjectileData so_ProjData;

	[SerializeField]
	private PlayerBullet projectilePrefab;

	[SerializeField]
	private float jumpHeight = 2;

	// Direction Vectors for look/target and move
	[SerializeField]
	private Vector2 LookDirection = Vector2.zero;

	[SerializeField]
	private Vector2 lastMoveDir = Vector2.zero;

	[SerializeField]
	private GameObject targetArrow;

	private readonly float gravity = -3.0f;

	private InputActionMap actionMap;
	private CharacterController controller;

	private int fireCnt;

	private bool isLookAtCursor;
	private bool isMove;
	private bool LastDirInput; // false = move; true = look
	private Vector2 lookInput;

	private float m_speed = 5;

	private Vector2 moveInput;
	private PlayerInput playerInput;
	private Vector3 velocity;

	private bool canInput;

	private void Awake()
	{
		canInput = true;

		controller = gameObject.GetComponent<CharacterController>();
		playerInput = gameObject.GetComponent<PlayerInput>();
		actionMap = playerInput.actions.FindActionMap("Player");
	}

	// Update is called once per frame
	private void Update()
	{
		if (!canInput) return;

		Vector3 move = new(moveInput.x, 0, moveInput.y);
		controller.Move(move * m_speed * Time.deltaTime);

		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		if (!so_controlDevice.Get())
		{
			isLookAtCursor = true;
			targetArrow.SetActive(true);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// ininite horizontal plane at player height
			Plane plane = new(Vector3.up, transform.position);

			// Check hit point and get direction
			if (plane.Raycast(ray, out float distance))
			{
				Vector3 hitPoint = ray.GetPoint(distance);
				Vector3 dir = hitPoint - transform.position;

				Vector2 direction = new(dir.x, dir.z);

				LookDirection = direction.normalized;

				LastDirInput = true;
			}
		}

		if (!isLookAtCursor && isMove) // if just move use LastMoveDir
		{
			BodyDirection(lastMoveDir);
		}
		else if (!isLookAtCursor && !isMove) // if target / Look action use LookDirection
		{
			if (LastDirInput)
			{
				AntennaDirection(LookDirection);
			}
			else
			{
				BodyDirection(lastMoveDir);
			}
		}
		else
		{
			AntennaDirection(LookDirection);
			BodyDirection(lastMoveDir);
		}
	}

	private void OnEnable()
	{
		GameManager.Instance.onPauseToggled += PauseInputs;

		so_Speed.OnValueChanged += changeSpeed;

		// register to actions
		actionMap.FindAction("Target").started += OnTarget;
		actionMap.FindAction("Scroll").started += OnScroll;
	}

	private void OnDisable()
	{
		GameManager.Instance.onPauseToggled -= PauseInputs;

		so_Speed.OnValueChanged -= changeSpeed;

		// unregister to actions
		actionMap.FindAction("Target").started -= OnTarget;
		actionMap.FindAction("Scroll").started -= OnScroll;
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (!canInput) return;

		isMove = true;
		moveInput = context.ReadValue<Vector2>();
		//Debug.Log($"Move input : {moveInput}");

		if (moveInput != Vector2.zero)
		{
			lastMoveDir = moveInput;
			LastDirInput = false;
		}
		else
		{
			isMove = false;
		}
	}


	public void OnLook(InputAction.CallbackContext context)
	{
		if (!canInput) return;

		if (so_controlDevice.Get()) //false = keyboard/mouse ; true = controller
		{
			lookInput = context.ReadValue<Vector2>();
			if (lookInput != Vector2.zero)
			{
				LookDirection = lookInput;
				isLookAtCursor = true;
				LastDirInput = true;
				targetArrow.SetActive(true);
			}
			else
			{
				isLookAtCursor = false;
				targetArrow.SetActive(false);
			}
		}
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (!canInput) return;

		//Debug.Log("Fire");

		fireCnt++;
		if (fireCnt % 3 == 0)
		{
			//Debug.Log("End Fire");

			fireCnt = 0;

			//Test Fire

			PlayerBullet pew = Instantiate(projectilePrefab, pewpew.transform.position,
				projectileDirection(LookDirection));
			pew.Frequency = playerDamagable.Frequency;
			//pew.GetComponent<BaseProjectile>().SetValues(so_ProjData.projectileBody, so_ProjData.speed, so_ProjData.dmg, so_ProjData.range, so_ProjData.impactRadius);
		}
		//Debug.Log("Active  Fire");
	}

	private void BodyDirection()
	{
		//Get angle from LookDirection
		float targetAngle = Mathf.Atan2(LookDirection.x, LookDirection.y) * Mathf.Rad2Deg;

		body.transform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
	}

	private void BodyDirection(Vector2 dir)
	{
		float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

		body.transform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);

		so_Angle.Set(targetAngle);
	}

	private void AntennaDirection(Vector2 dir)
	{
		float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

		antenna.transform.localRotation = Quaternion.Euler(-90f, targetAngle, 180f);

		so_Angle.Set(targetAngle);
	}

	private Quaternion projectileDirection(Vector2 dir)
	{
		float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

		return Quaternion.Euler(0f, targetAngle, 0f);
	}

	private void changeSpeed(float p_speed)
	{
		m_speed = p_speed;
	}

	private void PauseInputs(bool isPaused)
	{
		canInput = !isPaused;
	}

	#region mouse/keyboard specifics

	//test
	private void OnTarget(InputAction.CallbackContext context)
	{
		if (!canInput) return;

		//Debug.LogWarning("target mouse");

		//lastMoveDir = LookDirection;
	}

	private void OnScroll(InputAction.CallbackContext context)
	{
		if (!canInput) return;

		//Debug.Log(" test" + context.ReadValue<Vector2>());
		//TODO change frequence 

		playerDamagable.Frequency = playerDamagable.Frequency switch
		{
			Frequencies.Square => Frequencies.Triangle,
			Frequencies.Triangle => Frequencies.Wave,
			_ => Frequencies.Square
		};
	}

	private void MouseTarget()
	{
		if (actionMap.FindAction("Target").IsPressed())
		{
			isLookAtCursor = true;
			// use angle to set direction
		}
		else
		{
			isLookAtCursor = false;
		}
	}

	#endregion
}