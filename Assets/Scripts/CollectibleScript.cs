using UnityEngine;
using System.Collections;
using System;

public class CollectibleScript : MonoBehaviour {
    public int m_scoreAdded;
    float timeLife;
    int pos;
    public AudioClip soundBonus;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start () {
        timeLife = 0.0f;
    }

    float prefabF;

    // Update is called once per frame
    void Update () {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            LifeTime();
        }
	}

    void LifeTime()
    {
        timeLife += Time.deltaTime;
        if(timeLife >= 4f)
        {
            GameObject.FindWithTag("SpawnManagger").GetComponent<CollectiblesSpawning>().setFree(pos);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject go = GameObject.Find("Game Controller");
            GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
            if (prefabF <= 0.5f)
            {
                other.receivEvent(GameControllerScript.EEvent.autreEvent);
            }
            else if ((prefabF <= 3f) && (prefabF > 0.5f))
            {
                other.receivEvent(GameControllerScript.EEvent.chatplus);
            }
            else if ((prefabF <= 3.5f) && (prefabF > 3f))
            {
                other.receivEvent(GameControllerScript.EEvent.autreEvent);
            }
            else if ((prefabF <= 4.0f) && (prefabF > 3.5f))
            {
                other.receivEvent(GameControllerScript.EEvent.hentaiplus);
            }
            else if ((prefabF <= 4.2f) && (prefabF > 4.0f))
            {
                other.receivEvent(GameControllerScript.EEvent.autreEvent);
            }
            else if ((prefabF <= 4.4f) && (prefabF > 4.2f))
            {
                other.receivEvent(GameControllerScript.EEvent.autreEvent);
            }

            other.printScore(new Vector2(transform.position.x, transform.position.y), "+"+ m_scoreAdded);
            GameObject.FindWithTag("SpawnManagger").GetComponent<CollectiblesSpawning>().setFree(pos);


            go = GameObject.Find("Text");
            HUDscore hud = (HUDscore)go.GetComponent<HUDscore>();
            hud.AddScore(m_scoreAdded);

            Destroy(this.gameObject);
        }
    }

    public void addPos(int m_randomPosition)
    {
        pos = m_randomPosition;
    }

    internal void setType(float m_randomPrefab)
    {
        prefabF = m_randomPrefab;
    }
}