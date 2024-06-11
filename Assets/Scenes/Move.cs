using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	float speed = 5;
	float jump = 6F;
	List<Collider2D> GroundColliders = new List<Collider2D>();
	float x;

	// Start is called before the first frame update
	void Start() {
		x = transform.localScale.x;
	}

	// Update is called once per frame
	void Update() {
		var moveDelta = Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.Translate(moveDelta);

		if (moveDelta.x > 0) {
			Vector3 v = transform.localScale;
			v.x = x;
			transform.localScale = v;
		}
		else if (moveDelta.x < 0) {
			Vector3 v = transform.localScale;
			v.x = -x;
			transform.localScale = v;
		}

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

	bool IsGrounded() {
		return true;
	}
}
