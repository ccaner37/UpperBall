using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
	public GameObject ball;
	private float fov;
	public Camera cam;

	private float durationInSeconds = 40;
	private float currentTime;
	private float slowMotionFactor = 0.07f;
	private bool isGameOver = false;
	private ParticleSystem particles;

	void Start()
	{
		cam = this.GetComponent<Camera>();
		particles = ball.GetComponent<ParticleSystem>();
	}

	private void FixedUpdate()
	{
		if ((transform.position.y - 3.5f) - ball.transform.position.y < 0.20f)
		{
			transform.position = transform.position + Vector3.up * 1.2f * Time.fixedDeltaTime;
		}
		
	}

	public void GameOverEffect()
	{
		isGameOver = true;
		Time.timeScale = slowMotionFactor;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		particles.Play();
		StartCoroutine(NoMoreGameOverEffect());

	}
	private void Update()
	{
		if (isGameOver)
		{
			transform.LookAt(ball.transform);
			currentTime += Time.deltaTime / durationInSeconds;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 42, currentTime);
		}

	}

	private IEnumerator NoMoreGameOverEffect()
	{
		yield return new WaitForSeconds(0.45f);
		isGameOver = false;
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		transform.position = new Vector3(0, 4.53999996f, -5.4000001f);
		transform.eulerAngles = new Vector3(17.0000019f, 0, 0);
		cam.fieldOfView = 60;
		particles.Stop();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}

}
