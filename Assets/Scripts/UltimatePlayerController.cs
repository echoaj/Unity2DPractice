using UnityEngine;
using UnityEngine.Events;

// reference: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs

public class UltimatePlayerController : MonoBehaviour
{
	public ParticleSystem dust;

	[Header("Settings")]
	[SerializeField] private float JumpForce = 700f;								// Amount of force added when the player jumps.
	public float moveSpeed = 50f;
	[Range(0.02f, .2f)] [SerializeField] private float MoveSmoothing = .05f;		// How much to smooth out the movement

	[Header("Checks")]
	[SerializeField] private Transform GroundCheck;									// A position marking where to check if the player is grounded.
	[SerializeField] private Transform SideCheck;									// A position marking where to check if the player is grounded.
	[SerializeField] private LayerMask WhatIsGround;								// A mask determining what is ground to the character

	const float GroundedRadius = 0.2f;												// How close we can get to our empty ground object before we deterine touching ground
	private bool Grounded;															// Whether or not the player is grounded.
	private bool Sided;																// Whether or not we are touching the side.
	private bool FacingRight = true;												// For determining which way the player is currently facing.
	private Rigidbody2D Rigidbody2D;
	private Vector3 Velocity;														// Movement / Velocity

	private float horizontalMove = 0f;
	private bool jump = false;


	private void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// 2
	bool CheckCollision(Transform emptyGOCheck)
    {
		bool collides = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(emptyGOCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				collides = true;
			}
		}
		return collides;
    }

	// 1
	// no matter what frame rate it is it will be called same number of times per second
	// called before update
	private void FixedUpdate()
	{
		Grounded = CheckCollision(GroundCheck);
		Sided = CheckCollision(SideCheck);

		// 5
		// Move our character
		Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

	// 4
	// Update is called once per frame
	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
	}

	// 6
	public void Move(float move, bool jump)
	{
		if (!Sided)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MoveSmoothing);
		}

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !FacingRight)
		{ 
			Flip();			// ... flip the player.
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && FacingRight)
		{
			Flip();			// ... flip the player.
		}

		// If the player should jump...
		if (Grounded && jump)
		{
			// Add a vertical force to the player.
			Grounded = false;
			Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
			JumpAnimation();
		}
	}

	// 7
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void JumpAnimation()
    {
		dust.Play();
    }
}