using UnityEngine;

public class Move : MonoBehaviour {
	private float speed = 5;
	private float jump = 6F;

	// Start is called before the first frame update
	private void Start() {

	}

	// Update is called once per frame
	private void Update() {
		var moveDelta = Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.Translate(moveDelta);

		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			GetComponent<Rigidbody2D>().AddForce(transform.up * jump, ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			GetComponent<Rigidbody2D>().AddTorque(0.4F, ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			GetComponent<Rigidbody2D>().AddTorque(-0.4F, ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			transform.Rotate(new Vector3(0, 0, 180));
		}
	}

	private bool IsGrounded() {
		return true;
	}
}
