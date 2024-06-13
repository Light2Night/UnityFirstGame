using UnityEngine;

public class Move : MonoBehaviour {
	Animator animator;
	Rigidbody2D rigidbodyObj;

	float speed = 1000F;
	float walkingSpeedMultiplier = 0.5F;
	float jump = 6F;
	float flip;
	float flipGrounded = 0.5F;
	float flipInAir = 0.2F;
	float x;
	float groundCheckDistance = 0.03f;
	bool isGrounded;
	bool isRunning;
	bool isWalking;
	float horizontalSpeed;

	// Start is called before the first frame update
	void Start() {
		animator = GetComponent<Animator>();
		rigidbodyObj = GetComponent<Rigidbody2D>();
		x = transform.localScale.x;
		isRunning = false;
	}

	// Update is called once per frame
	void Update() {
		isGrounded = IsGrounded();
		isRunning = false;
		isWalking = false;
		flip = isGrounded ? flipGrounded : flipInAir;
		horizontalSpeed = rigidbodyObj.velocity.x;

		if (isGrounded) {
			if (Input.GetKey(KeyCode.A)) {
				if (Input.GetKey(KeyCode.S)) {
					if (Mathf.Abs(horizontalSpeed) < 3)
						MoveHorizontally(-1, walkingSpeedMultiplier);
					isWalking = true;
				}
				else {
					MoveHorizontally(-1);
					isRunning = true;
				}
			}
			if (Input.GetKey(KeyCode.D)) {
				if (Input.GetKey(KeyCode.S)) {
					if (Mathf.Abs(horizontalSpeed) < 3)
						MoveHorizontally(1, walkingSpeedMultiplier);
					isWalking = true;
				}
				else {
					MoveHorizontally(1);
					isRunning = true;
				}
			}


			if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) {
				rigidbodyObj.AddForce(transform.up * jump, ForceMode2D.Impulse);
			}
		}

		animator.SetBool("IsRunning", isRunning);
		animator.SetBool("IsWalking", isWalking);
		animator.SetBool("IsGrounded", isGrounded);

		if (Input.GetKeyDown(KeyCode.Q)) {
			rigidbodyObj.AddTorque(flip, ForceMode2D.Impulse);
			animator.SetBool("IsRunning", false);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			rigidbodyObj.AddTorque(-flip, ForceMode2D.Impulse);
			animator.SetBool("IsRunning", false);
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			transform.Rotate(new Vector3(0, 0, 180));
		}

		//Debug.DrawLine(transform.position, -transform.up * 5, Color.blue, 2.0f, false);
	}

	bool IsGrounded() {
		Collider2D collider = GetComponent<Collider2D>();
		Vector2 bottom = collider.bounds.center - new Vector3(0, collider.bounds.extents.y + groundCheckDistance, 0);

		RaycastHit2D hit = Physics2D.Raycast(bottom, Vector2.down, groundCheckDistance);

		Debug.DrawLine(bottom, bottom + Vector2.down * groundCheckDistance, Color.green, 5F);

		return hit.collider != null;
	}

	void MoveHorizontally(int sign, float speedMultiplier = 1) {
		rigidbodyObj.AddForce((speed * speedMultiplier) * sign * Time.deltaTime * transform.right);

		Vector3 v = transform.localScale;
		v.x = sign * x;
		transform.localScale = v;
	}
}
