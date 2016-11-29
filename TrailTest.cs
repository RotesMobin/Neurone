using UnityEngine;
using System.Collections;

public class TrailTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mvt = new Vector3(1, 1, 0);
        transform.Translate(mvt * Time.deltaTime);
    }
}
