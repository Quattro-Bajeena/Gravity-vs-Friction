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

	[SerializeField] bool grounded;
	[SerializeField] Vector2 velocity = Vector2.zero;

	[SerializeField] float currentAirVelocityLimit;
	

	[Header("Modes")]

	[Header("Gravity")]
	public bool gravityMode;
	[SerializeField] float gravityModeGravity;
	[SerializeField] float gravityModeAcceleration;
	[SerializeField] float gravityModeFriction;

	[Header("Friction")]
	public bool frictionMode;
	[SerializeField] float frictionModeGravity;
	[SerializeField] float frictionModeAcceleration;
	[SerializeField] float frictionModeFriction;


	
	BoxCollider2D boxCollider;

    void Awake()
    {
		character = GetComponent<Character>();
		boxCollider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {

		//
		frictionMode = false;
		gravityMode = false;
		

		if (Input.GetMouseButton(0))
			frictionMode = true;
		else if (Input.GetMouseButton(1))
			gravityMode = true;

		if(grounded == true)
		{
			currentAirVelocityLimit = float.MaxValue;
			velocity.y = 0;
			if (Input.GetButtonDown("Jump"))
			{
				//velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravity));
				velocity.y = jumpVelocity;

				currentAirVelocityLimit = Mathf.Clamp( Mathf.Abs(velocity.x), minAirVelocityLimit, float.MaxValue);
			}

		}

		
		//
		float moveInput = Input.GetAxisRaw("Horizontal");

		
		float acceleration = grounded ? defaultWalkAcceleration : defaultAirAcceleration;
		float friction;
		float gravity;

		if (frictionMode == true && gravityMode == false)
		{
			friction = grounded ? frictionModeFriction : 0;
			gravity = frictionModeGravity;
		}
		else if (frictionMode == false && gravityMode == true)
		{
			friction = grounded ? gravityModeFriction : 0;
			gravity = gravityModeGravity;
		}
		else
		{
			friction = grounded ? defaultFriction : 0;
			gravity = defaultGravity;
		}
		
		velocity.y -= gravity * Time.deltaTime;

		velocity.x += moveInput * acceleration * Time.deltaTime;
		velocity.x -= velocity.x * friction;
		velocity.x = Mathf.Clamp(velocity.x, -currentAirVelocityLimit, currentAirVelocityLimit);

		transform.Translate(velocity * Time.deltaTime);

		//Colison detection

		Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f);

		grounded = false;
		bool hitWall = false;
		bool hitFloor = false;

		foreach (Collider2D hit in hits)
		{

			if (hit == boxCollider)
				continue;

			if (hit.isTrigger)
				continue;
				

			ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

			Tilemap tilemap = hit.GetComponentInChildren<Tilemap>();
			if (tilemap)
			{
				Grid grid = tilemap.layoutGrid;
				Vector3Int pos = grid.WorldToCell(colliderDistance.pointB);
				CustomTile tile = tilemap.GetTile(pos) as CustomTile;
				if (tile)
				{
					Debug.Log("Its custom tile");
					tile.OnCollision(character);
				}
				
				

			}

			

			

			if (colliderDistance.isOverlapped == true)
			{
				transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

				if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
				{
					if(hitFloor == false)
					{
						character.Health.HitFloorDamage(Mathf.Abs(velocity.y));
						hitFloor = true;
					}
					velocity.y = 0;
					grounded = true;

				}
				else if (Vector2.Angle(colliderDistance.normal, Vector2.up) > 90 && velocity.y > 0)
				{
					if(hitWall == false)
					{
						character.Health.HitWallDamage(Mathf.Abs(velocity.y));
						hitWall = true;
					}
					
					velocity.y = 0;
				}
				else if (Vector2.Angle(colliderDistance.normal, Vector2.up) == 90 && velocity.y < 0)
				{
					if (hitWall == false)
					{
						character.Health.HitWallDamage(Mathf.Abs(velocity.x));
						hitWall = true;
					}
					velocity.x = 0;
				}
			}
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("on collison enter 2d");
		Tilemap tilemap =  collision.gameObject.GetComponent<Tilemap>();
		if (tilemap)
			Debug.Log("we hit tilemap");
	}





}
