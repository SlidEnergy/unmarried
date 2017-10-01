using UnityEngine;
using System.Collections;

public class LadyController : MonoBehaviour {

	public Transform player;
	
	public float enemySpeed = 1;
	public float followingDistance = 3;
	public float turnSpeed = 5;

	private Camera mainCamera;

	private Transform thisTransform;

	private Transform spawnPoint;

	private Rigidbody2D thisRigidbody;

	void Start () {

		mainCamera = Camera.main;
		thisTransform = transform;
		thisRigidbody = GetComponent<Rigidbody2D>();

		spawnPoint = GameObject.Find("SpawnPoint").transform;

		MoveLeft();
	}
	
	void Update () {

		if (Vector2.Distance(thisTransform.position, player.position) < followingDistance)
		{
			FollowPlayer();
		}
		else
		{
			MoveLeft();
		}
	}

	private void FollowPlayer()
	{
		//transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
		thisTransform.position = Vector2.MoveTowards(thisTransform.position, player.position, enemySpeed * Time.deltaTime);
		//transform.position += transform.forward * speed * Time.deltaTime;

		// Отключаем скорость движения влево
		thisRigidbody.velocity = new Vector2(0, 0);

		// Поворачиваем спрайт
		Vector3 moveDirection = thisTransform.position - player.position;

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		thisTransform.rotation =
		  Quaternion.Slerp(thisTransform.rotation,
							Quaternion.Euler(0, 0, targetAngle),
							turnSpeed * Time.deltaTime);
	}

	private void MoveLeft()
	{ 
		// Включаем скорость движения влево
		thisRigidbody.velocity = new Vector2(-1 * enemySpeed, 0);

		// Поворачиваем спрайт
		Vector3 moveDirection = thisTransform.position - Vector3.left;

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		thisTransform.rotation =
			Quaternion.Slerp(thisTransform.rotation,
							Quaternion.Euler(0, 0, targetAngle),
							turnSpeed * Time.deltaTime);
	}

	void OnBecameInvisible()
	{
		if (mainCamera == null)
			return;

		float yMax = mainCamera.orthographicSize - 0.5f;
		thisTransform.position = new Vector3(spawnPoint.position.x, Random.Range(-yMax, yMax), thisTransform.position.z);
	}
}
