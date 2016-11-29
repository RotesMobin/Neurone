using UnityEngine;
using System.Collections;


public class UmbrellaScript : MonoBehaviour {
    int pos;
    float timeLife;
    public AudioClip soundBonus;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    GameControllerScript scrpt;
    // Use this for initialization
    void Start () {
        timeLife = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime();
    }

    void LifeTime()
    {
        timeLife += Time.deltaTime;
        if (timeLife >= 4f)
        {
            GameObject.FindWithTag("SpawnManagger").GetComponent<CollectiblesSpawning>().setFree(pos);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("UmbrellaProtection");

            GameObject go = GameObject.Find("Game Controller");
            scrpt = go.GetComponent<GameControllerScript>();
            scrpt.receivEvent(GameControllerScript.EEvent.autreEvent);

            GameObject.FindWithTag("SpawnManagger").GetComponent<CollectiblesSpawning>().setFree(pos);

            source.PlayOneShot(soundBonus, 1.0f);

            Destroy(this.gameObject);
        }
    }

    public void addPos(int m_randomPosition)
    {
        pos = m_randomPosition;
    }
}
