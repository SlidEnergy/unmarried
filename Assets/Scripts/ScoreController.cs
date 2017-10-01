using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	private BoyController boy;
	private Text text;

	// Use this for initialization
	void Start () {

		this.boy = GameObject.Find("boy").GetComponent<BoyController>();
		this.text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		this.text.text = boy.CointCount.ToString();
	}
}
