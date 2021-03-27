using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

	[SerializeField] private float thrust = 50f;
	[SerializeField] private Rigidbody rb;
	private Vector3 currentMousePos;
	private Vector3 mousePos2;
	private float wallDistance = 1.7f;
	Vector3 deltaPos = Vector3.zero;
	public CameraController camcontrol;

	// Start is called before the first frame update
	void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 currentMousePos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 lastMousePos = Input.mousePosition;
			deltaPos = currentMousePos - lastMousePos;
			//Debug.Log(deltaPos);
		
			Vector3 force = new Vector3(-deltaPos.x, -deltaPos.y, 0) * thrust;

		
			rb.AddForce(force, ForceMode.Impulse);
		}
	}

	private void LateUpdate()
	{
		Vector3 pos = transform.position;

		if (transform.position.x > wallDistance)
		{
			pos.x = wallDistance;
		}

		if(transform.position.x < -wallDistance)
		{
			pos.x = -wallDistance;
		}

		transform.position = pos;

	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "GameOverZone")
		{
			Debug.Log("GameOver");
			camcontrol.GameOverEffect();

		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "GameOverZone")
		{
			Debug.Log("GameOver");
			camcontrol.GameOverEffect();

		}
	}

}
