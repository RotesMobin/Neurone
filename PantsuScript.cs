using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PantsuScript : MonoBehaviour
{
    float animationTime;
    float animationStopTime;

    Vector3 scaleValue;
    Vector3 rotationValue;
    float crtTime;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        animationTime = 0f;
        animationStopTime = 1.5f;

        scaleValue = new Vector3(0.004f, 0.004f, 0f);
        rotationValue = new Vector3(0f, 0f, 2f);
        crtTime = 0;

        Color tmp = this.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        this.GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        crtTime += Time.deltaTime;
        if(crtTime > 1.0f)
        {
            animationTime += Time.deltaTime;
            if (animationTime <= animationStopTime)
            {
                //this.transform.Rotate( rotationValue * 2);
                this.transform.Rotate(Vector3.forward * Time.deltaTime * 10);
                //this.transform.localScale += scaleValue * 2;
                Color tmp = this.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.0f + (animationTime/ animationStopTime);
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = this.GetComponent<SpriteRenderer>().color;
                tmp.a = 1.0f;
                GameObject.Find("HUD_Restart").GetComponent<SpriteRenderer>().color = tmp;
                tmp = GameObject.Find("HUD_Restart").GetComponent<SpriteRenderer>().color;
                tmp.a = 1.0f;
                this.GetComponent<SpriteRenderer>().color = tmp;
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                }
            }
        }
    }
}
