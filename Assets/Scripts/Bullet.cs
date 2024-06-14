using UnityEngine;

public class Bullet : MonoBehaviour {
	public Rigidbody2D rigidbodyObj;
	float speed = 10F;

	// Start is called before the first frame update
	void Start() {
		rigidbodyObj.AddForce(speed * transform.right);
	}

	// Update is called once per frame
	void Update() {

	}
}
