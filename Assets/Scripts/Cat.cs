using UnityEngine;

public class Move : MonoBehaviour {
	public Rigidbody2D rigidbodyObj;
	public Collider2D colliderObj;
	public Animator animatorObj;

	float speed = 1000F;
	float walkingSpeedMultiplier = 0.5F;
	float jump = 6F;
	float flip;
	float flipGrounded = 0.5F;
	float flipInAir = 0.2F;
	float localScaleX;
	float groundCheckDistance = 0.03f;
	bool isGrounded;
	bool isRunning;
	bool isWalking;
	float horizontalSpeed;

	// Start is called before the first frame update
	void Start() {
		localScaleX = transform.localScale.x;
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
				rigidbodyObj.AddForce(jump * transform.up, ForceMode2D.Impulse);
			}
		}

		animatorObj.SetBool("IsRunning", isRunning);
		animatorObj.SetBool("IsWalking", isWalking);
		animatorObj.SetBool("IsGrounded", isGrounded);

		if (Input.GetKeyDown(KeyCode.Q)) {
			rigidbodyObj.AddTorque(flip, ForceMode2D.Impulse);
			animatorObj.SetBool("IsRunning", false);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			rigidbodyObj.AddTorque(-flip, ForceMode2D.Impulse);
			animatorObj.SetBool("IsRunning", false);
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			transform.Rotate(new Vector3(0, 0, 180));

			var scale = transform.localScale;
			scale.x = -scale.x;
			transform.localScale = scale;
		}
	}

	bool IsGrounded() {
		Vector2 bottom = colliderObj.bounds.center - new Vector3(0, colliderObj.bounds.extents.y + groundCheckDistance, 0);

		RaycastHit2D hit = Physics2D.Raycast(bottom, Vector2.down, groundCheckDistance);

		//Debug.DrawLine(bottom, bottom + Vector2.down * groundCheckDistance, Color.green, 5F);

		return hit.collider != null;
	}

	void MoveHorizontally(int sign, float speedMultiplier = 1) {
		rigidbodyObj.AddForce(speed * speedMultiplier * sign * Time.deltaTime * transform.right);

		Vector3 v = transform.localScale;
		v.x = sign * localScaleX;
		transform.localScale = v;
	}
}
