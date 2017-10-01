using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoyController : MonoBehaviour {

	public int CointCount;

	public float moveSpeed;
	private Vector3 moveDirection;

	public float turnSpeed;

	private bool isInvincible = false;
	private float timeSpentInvincible;

	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int currentColliderIndex = 0;

	private int lives = 1;

	public AudioClip enemyContactSound;
	public AudioClip coinContactSound;

	private Transform thisTransform;

	private Renderer thisRenderer;

	// Use this for initialization
	void Start () {

		moveDirection = Vector3.right;
		thisTransform = transform;
		thisRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// 1
		Vector3 currentPosition = thisTransform.position;
		// 2
		if (Input.GetButton("Fire1"))
		{
			// 3
			Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// 4
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0;
			moveDirection.Normalize();
		}

		Vector3 target = moveDirection * moveSpeed + currentPosition;
		thisTransform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation =
		  Quaternion.Slerp(transform.rotation,
							Quaternion.Euler(0, 0, targetAngle),
							turnSpeed * Time.deltaTime);

		EnforceBounds();

		//1
		if (isInvincible)
		{
			//2
			timeSpentInvincible += Time.deltaTime;

			//3
			if (timeSpentInvincible < 3f)
			{
				float remainder = timeSpentInvincible % .3f;
				thisRenderer.enabled = remainder > .15f;
			}
			//4
			else
			{
				thisRenderer.enabled = true;
				isInvincible = false;
			}
		}
	}

	public void SetColliderForSprite(int spriteNum)
	{
		colliders[currentColliderIndex].enabled = false;
		currentColliderIndex = spriteNum;
		colliders[currentColliderIndex].enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("coin"))
		{
			GetComponent<AudioSource>().PlayOneShot(coinContactSound);

			other.transform.GetComponent<CoinController>().Collect();
			CointCount++;

			if (CointCount >= 100)
			{
				Application.LoadLevel("WinScene");
			}
		}
		else if (!isInvincible && other.CompareTag("lady"))
		{
			GetComponent<AudioSource>().PlayOneShot(enemyContactSound);

			isInvincible = true;
			timeSpentInvincible = 0;

			if (--lives <= 0)
			{
				Application.LoadLevel("LoseScene");
			}
		}
	}

	private void EnforceBounds()
	{
		// 1
		Vector3 newPosition = thisTransform.position;
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		// 2
		float xDist = mainCamera.aspect * mainCamera.orthographicSize;
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		// 3
		if (newPosition.x < xMin || newPosition.x > xMax)
		{
			newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
			moveDirection.x = -moveDirection.x;
		}
		// TODO vertical bounds

		// 4
		thisTransform.position = newPosition;

		float yMax = mainCamera.orthographicSize;

		if (newPosition.y < -yMax || newPosition.y > yMax)
		{
			newPosition.y = Mathf.Clamp(newPosition.y, -yMax, yMax);
			moveDirection.y = -moveDirection.y;
		}
	}
}
