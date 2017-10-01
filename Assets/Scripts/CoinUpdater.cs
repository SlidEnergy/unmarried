using UnityEngine;

public class CoinUpdater : MonoBehaviour
{
	private CoinController cointController;

	// Use this for initialization
	void Start()
	{
		cointController = transform.GetComponent<CoinController>();
	}

	void OnBecameInvisible()
	{
		cointController.OnBecameInvisible();
	}
}