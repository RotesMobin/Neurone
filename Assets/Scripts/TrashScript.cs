using UnityEngine;
using System.Collections;

public class TrashScript : MonoBehaviour {
    public AudioClip soundMalus;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    PlayerController player;

    Animator anim;

    bool stunnedPlayer;
    bool spawnLeft;

    public float currentSpeed;

    float minX = -9.65f;
    float maxX = 9.65f;
    float minY = -5.45f;
    float maxY = 5.45f;

    public Vector2 startPos;
    public Vector2 endPos;

    // Use this for initialization
    void Start () {
        stunnedPlayer = false;
        player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frames
	void Update () {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            updateStraigth();
            if (spawnLeft == true)
            {
                anim.SetInteger("Animation", 0);
            }
            else
            {
                anim.SetInteger("Animation", 1);
            }
        }
        else
        {
            anim.Stop();
        }
    }

    internal void StartStraigth()
    {
        //
        // Random start pos
        float xPosS = 0.0f;
        float yPosS = 0.0f;
        float xPosE = 0.0f;
        float yPosE = 0.0f;

        switch (Random.Range(1, 3))
        {
            case 1:
                xPosS = minX - 2;
                yPosS = (float)Random.Range(-450, -150) / 100.0f;
                xPosE = maxX + 2;
                yPosE = yPosS;
                spawnLeft = true;
                break;
            case 2:
                xPosS = maxX + 2;
                yPosS = (float)Random.Range(-450, -150) / 100.0f;
                xPosE = minX - 2;
                yPosE = yPosS;
                spawnLeft = false;
                break;
        }
        startPos = new Vector3(xPosS, yPosS, -0.5f);
        endPos = new Vector3(xPosE, yPosE, -0.5f);

        transform.position = startPos;
        currentSpeed = 2.0f;
    }

    void updateStraigth()
    {
        //
        // move from start to end
        Vector3 mvt = new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, -0.5f);
        mvt.Normalize();
        transform.position = new Vector3(transform.position.x + mvt.x * currentSpeed * Time.deltaTime, transform.position.y + mvt.y * currentSpeed * Time.deltaTime, -0.5f);


        //
        // Check if we are out of screen

        if (transform.position.x < minX - 2
            || transform.position.y < minY - 2
            || transform.position.x > maxX + 2
            || transform.position.y > maxY + 2)
        {
            //transform.position = startPos;
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (player.isProtected == false)
        {
            if (stunnedPlayer == false)
            {
                if (coll.gameObject.tag == "Player")
                {
                    GameObject go = GameObject.Find("Game Controller");
                    GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
                    other.receivEvent(GameControllerScript.EEvent.poubelleplus);

                    coll.gameObject.SendMessage("Stunning");
                    stunnedPlayer = true;

                    source.PlayOneShot(soundMalus, 1.0f);


                    Destroy(this.gameObject);
                }
            }
        }
    }
}