using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CharacterController : MonoBehaviour
{
	Character character;

	[SerializeField] float jumpVelocity;
	[SerializeField] float defaultWalkAcceleration;
	[SerializeField] float defaultAirAcceleration;
	[SerializeField] float defaultFriction;
	[SerializeField] float defaultGravity;
	[SerializeField] float minAirVelocityLimit;

	[SerializeField] Vector2 velocityLimit;

	[SerializeField] bool grounded;
	[SerializeField] Vector2 velocity = Vector2.zero;
	public Vector2 Velocity
	{
		get { return velocity; }
	}

	public float VelocityUp
	{
		get { return velocity.y; }
		set { velocity.y = value; }
	}

	[SerializeField] float currentAirVelocityLimit;
	public enum MovementMode
	{
		Normal,
		NoGravity,
		NoFriction
	}

	public MovementMode mode;

	[Header("Modes")]

	[Header("Gravity")]
	[SerializeField] float gravityModeGravity;
	[SerializeField] float gravityModeAcceleration;
	[SerializeField] float gravityModeFriction;

	[Header("Friction")]
	[SerializeField] float frictionModeGravity;
	[SerializeField] float frictionModeAcceleration;
	[SerializeField] float frictionModeFriction;


	public float FrictionMultiplier { get; set; } = 1;
	
	BoxCollider2D boxCollider;
	SpriteRenderer spriteRenderer;

    void Awake()
    {
		
		character = GetComponent<Character>();
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		currentAirVelocityLimit = minAirVelocityLimit;
	}

	void Update()
    {
		ModeInput();
		CheckGrounded();
		Movement();
		CollisionDetection();
	}

	void ModeInput()
	{
		mode = MovementMode.Normal;

		if (Input.GetMouseButton(0))
			mode = MovementMode.NoFriction;
		else if (Input.GetMouseButton(1))
			mode = MovementMode.NoGravity;

	}

	void CheckGrounded()
	{
		if (grounded == true)
		{
			currentAirVelocityLimit = float.MaxValue;
			velocity.y = 0;
			if (Input.GetButtonDown("Jump"))
			{
				//velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravity));
				velocity.y = jumpVelocity;
				grounded = false;
				currentAirVelocityLimit = Mathf.Clamp(Mathf.Abs(velocity.x), minAirVelocityLimit, float.MaxValue);
			}

		}
	}

	void Movement()
	{
		float moveInput = Input.GetAxisRaw("Horizontal");


		float acceleration = grounded ? defaultWalkAcceleration : defaultAirAcceleration;
		float friction = defaultFriction;
		float gravity = defaultGravity;

		switch (mode)
		{
			case MovementMode.Normal:
				friction = grounded ? defaultFriction : 0;
				gravity = defaultGravity;
				break;
			case MovementMode.NoGravity:
				friction = grounded ? gravityModeFriction : 0;
				gravity = gravityModeGravity;
				break;
			case MovementMode.NoFriction:
				friction = grounded ? frictionModeFriction : 0;
				gravity = frictionModeGravity;
				break;
			default:
				break;
		}
		friction *= FrictionMultiplier;

		FrictionMultiplier = 1;


		velocity.y -= gravity * Time.deltaTime;

		velocity.x += moveInput * acceleration * Time.deltaTime;
		velocity.x -= velocity.x * friction * Time.deltaTime;

		velocity.x = Mathf.Clamp(velocity.x, -currentAirVelocityLimit, currentAirVelocityLimit);

		if (velocity.x >= velocityLimit.x)
			Debug.Log("Velocity x Over limit");
		if (velocity.y >= velocityLimit.y)
			Debug.Log("velocity y over limit");

		velocity.x = Mathf.Clamp(velocity.x, -velocityLimit.x, velocityLimit.x);
		velocity.y = Mathf.Clamp(velocity.y, -velocityLimit.y, velocityLimit.y);

		

		transform.Translate(velocity * Time.deltaTime);

		
	}

	void CollisionDetection()
	{
		Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f);
		grounded = false;

		foreach (Collider2D hit in hits)
		{
			if (hit.isTrigger)
				continue;

			if (hit == boxCollider)
				continue;

			ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
			//pointA - tile
			//pointB - player


			Tilemap tilemap = hit.GetComponentInChildren<Tilemap>();
			if (tilemap)
			{
				Grid grid = tilemap.layoutGrid;

				Vector3Int pos = grid.WorldToCell(colliderDistance.pointA - colliderDistance.normal * 0.5f);
				TileBase tile = tilemap.GetTile(pos);


				if (tile == null)
				{
					Debug.LogWarning("no tile");
				}

				else if (tile as CustomTile)
				{
					CustomTile customTile = tile as CustomTile;
					customTile.OnCollision(character);
				}


			}
			else Debug.LogWarning("No tilemap");


			if (colliderDistance.isOverlapped == true)
			{
				
				transform.Translate( -1 * colliderDistance.normal * colliderDistance.distance);

				if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
				{
					character.Health.HitFloorDamage(Mathf.Abs(velocity.y));
					velocity.y = 0;
					grounded = true;

				}
				else if (Vector2.Angle(colliderDistance.normal, Vector2.up) > 90 && velocity.y > 0)
				{
					character.Health.HitWallDamage(Mathf.Abs(velocity.y));
					velocity.y = 0;
				}
				else if (Vector2.Angle(colliderDistance.normal, Vector2.up) == 90 && velocity.y < 0)
				{
					character.Health.HitWallDamage(Mathf.Abs(velocity.x));
					velocity.x = 0;
				}

				
			}



		}
	}


}
