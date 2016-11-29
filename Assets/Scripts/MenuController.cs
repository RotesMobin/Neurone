using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public AudioClip menuMusic;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    bool isCredit;
    bool isTuto;
    // Use this for initialization
    void Start () {
        Color tmp = GameObject.Find("imgCredit").GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        GameObject.Find("imgCredit").GetComponent<SpriteRenderer>().color = tmp;

        tmp = GameObject.Find("imgTuto").GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        GameObject.Find("imgTuto").GetComponent<SpriteRenderer>().color = tmp;

        tmp = GameObject.Find("HUD_Continue").GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        GameObject.Find("HUD_Continue").GetComponent<SpriteRenderer>().color = tmp;

        

        isCredit = false;
        isTuto = false;
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (isCredit && Input.GetMouseButtonDown(0))
        {
            Color tmp = GameObject.Find("imgCredit").GetComponent<SpriteRenderer>().color;
            tmp.a = 0.0f;
            GameObject.Find("imgCredit").GetComponent<SpriteRenderer>().color = tmp;

            isCredit = false;
        }
        else if(isTuto && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }

    void Play()
    {
        if (!isCredit)
        {
            Color tmp = GameObject.Find("imgTuto").GetComponent<SpriteRenderer>().color;
            tmp.a = 1.0f;
            GameObject.Find("imgTuto").GetComponent<SpriteRenderer>().color = tmp;

            tmp = GameObject.Find("HUD_Continue").GetComponent<SpriteRenderer>().color;
            tmp.a = 1.0f;
            GameObject.Find("HUD_Continue").GetComponent<SpriteRenderer>().color = tmp;

            isTuto = true;
        }
    }

    void Credit()
    {
        Color tmp = GameObject.Find("imgCredit").GetComponent<SpriteRenderer>().color;
        tmp.a = 1.0f;
        GameObject.Find("imgCredit").GetComponent<SpriteRenderer>().color = tmp;

        isCredit = true;
    }

    void Quit()
    {
        if (!isCredit)
            Application.Quit();
    }
}
