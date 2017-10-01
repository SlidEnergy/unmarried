using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	public float moveSpeed;

	private bool isZombie;

	private Transform countCounter;

	void Start()
	{
		this.countCounter = GameObject.Find("ScoreImage").GetComponent<Transform>();
	}

	void Update()
	{
		//1
		if (isZombie)
		{
			//2
			Vector3 currentPosition = transform.position;
			Vector3 counterPosition = Camera.main.ScreenToWorldPoint(countCounter.position);
			counterPosition.z = -1;
			Vector3 moveDirection = counterPosition - currentPosition;
			
			//4
			float distanceToTarget = moveDirection.magnitude;
			if (distanceToTarget > 0.3f)
			{
				//5
				if (distanceToTarget > moveSpeed)
					distanceToTarget = moveSpeed;

				//6
				moveDirection.Normalize();
				Vector3 target = moveDirection * distanceToTarget + currentPosition;
				transform.position =
				  Vector3.Lerp(currentPosition, target, moveSpeed * Time.deltaTime);
			}
			else
				Destroy(gameObject);
		}
	}

	public void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	public void Collect()
	{
		isZombie = true;

		GetComponent<Collider2D>().enabled = false;
	}
}
