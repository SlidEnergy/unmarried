using UnityEngine;

public class CoinCreator : MonoBehaviour {

	public GameObject coinPrefab;

	//1
	public float minSpawnTime = 0.75f;
	public float maxSpawnTime = 2f;

	//2    
	void Start()
	{
		Invoke("SpawnCoin", minSpawnTime);
	}

	//3
	void SpawnCoin()
	{
		// 1
		Camera camera = Camera.main;
		Vector3 cameraPos = camera.transform.position;
		float xMax = camera.aspect * camera.orthographicSize;
		float xRange = camera.aspect * camera.orthographicSize * 1.75f;
		float yMax = camera.orthographicSize - 0.5f;

		// 2
		Vector3 coinPos = new Vector3(cameraPos.x + Random.Range(xMax - xRange, xMax),
					  Random.Range(-yMax, yMax),
					  coinPrefab.transform.position.z);

		// 3
		Instantiate(coinPrefab, coinPos, Quaternion.identity);
		Invoke("SpawnCoin", Random.Range(minSpawnTime, maxSpawnTime));
	}
}
