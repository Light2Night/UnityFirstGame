using UnityEngine;

public class Move : MonoBehaviour {
	float speed = 1000F;
	float jump = 6F;
	float flip = 0.3F;
	float x;
	float groundCheckDistance = 0.03f;
	Animator animator;
	bool isRunning;

	// Start is called before the first frame update
	void Start() {
		x = transform.localScale.x;
		animator = GetComponent<Animator>();
		isRunning = false;
	}

	// Update is called once per frame
	void Update() {
		if (IsGrounded()) {
			isRunning = false;
			if (Input.GetKey(KeyCode.A)) {
				MoveHorizontally(-1);
				isRunning = true;
			}
			if (Input.GetKey(KeyCode.D)) {
				MoveHorizontally(1);
				isRunning = true;
			}

			animator.SetBool("IsRunning", isRunning);

			if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) {
				GetComponent<Rigidbody2D>().AddForce(transform.up * jump, ForceMode2D.Impulse);
			}
		}


		if (Input.GetKeyDown(KeyCode.Q)) {
			GetComponent<Rigidbody2D>().AddTorque(flip, ForceMode2D.Impulse);
			animator.SetBool("IsRunning", false);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			GetComponent<Rigidbody2D>().AddTorque(-flip, ForceMode2D.Impulse);
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

	void MoveHorizontally(int sign) {
		GetComponent<Rigidbody2D>().AddForce(sign * transform.right * speed * Time.deltaTime);

		Vector3 v = transform.localScale;
		v.x = sign * x;
		transform.localScale = v;
	}
}
