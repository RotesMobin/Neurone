using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
    public GameObject PlayerPrefab;

	// Use this for initialization
	void Start () {
        GameObject LoliChick = Instantiate(PlayerPrefab, transform.position, transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
