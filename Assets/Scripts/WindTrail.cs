using UnityEngine;
using System.Collections;
using System;

public class WindTrail : MonoBehaviour {
    public AudioClip soundWind;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    bool m_isOver;
    public float time;

    PlayerController player;

    public void launch(Vector3 position)
    {
        //
        // Change size and rotation
        float xPosDelta = UnityEngine.Random.Range(-0.5f, 0.5f);
        float yPosDelta = UnityEngine.Random.Range(-0.5f, 0.5f);
        transform.position = new Vector3(position.x - xPosDelta, position.y - yPosDelta, position.z + 1);

        float scaleDelta = UnityEngine.Random.Range(0.05f, 0.08f);
        Vector3 scale = new Vector3(scaleDelta, scaleDelta, 1f);
        transform.localScale = scale;   

        float minAngle = 0.0F;
        float maxAngle = 300;
        transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(minAngle, maxAngle));

        m_isOver = false;
        GetComponent<CircleCollider2D>().enabled = true;

        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1.0f;
        GetComponent<SpriteRenderer>().color = tmp;

        GetComponent<CircleCollider2D>().enabled = true;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
	}

    // Update is called once per frame
    void Update() {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            //
            // If active, fade off
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = tmp.a - Time.deltaTime * time;
            GetComponent<SpriteRenderer>().color = tmp;

            if (tmp.a < 0.8f)
            {
                GetComponent<CircleCollider2D>().enabled = false;
            }

            if (tmp.a < 0.0f)
            {
                m_isOver = true;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    internal bool isOver()
    {
        return m_isOver;    
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
       if (player.isProtected == false)
       {
            if (coll.gameObject.tag == "Player")
            {
                source.PlayOneShot(soundWind, 1.0f);

                coll.gameObject.SendMessage("DiePlayer");
            }
       }    
    }
}
