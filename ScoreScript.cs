using UnityEngine;
using System.Collections;
using System;

public class ScoreScript : MonoBehaviour {

    public float timeOnScreen;
    float timePassed;
    // Use this for initialization
    void Start () {
        if (timeOnScreen == 0)
        {
            timeOnScreen = 2;
        }
        timePassed = 0.0f;
        //this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        timePassed += Time.deltaTime;
        //
        // If active, fade off
        Color tmp = GetComponent<TextMesh>().color;
        tmp.a = 1.2f - timePassed / timeOnScreen;
        
        if (tmp.a > 1.0f)
        {
            tmp.a = 1.0f;
        }
        GetComponent<TextMesh>().color = tmp;
        if (tmp.a < 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void StartAff(Vector2 position, string v)
    {
        GetComponent<TextMesh>().text = v;

        transform.position = new Vector3(position.x, position.y, -25);
        Color tmp = GetComponent<TextMesh>().color;
        tmp.a = 1.0f;
        GetComponent<TextMesh>().color = tmp;

        if (timeOnScreen == 0)
        {
            timeOnScreen = 2;
        }
        timePassed = 0.0f;
    }
}
