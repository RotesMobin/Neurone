using UnityEngine;
using System.Collections;

public class PaperScript : MonoBehaviour {
    public AudioClip soundMalus;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    PlayerController player;

    public float currentSpeed;

    float minX = -9.65f;
    float maxX = 9.65f;
    float minY = -5.45f;
    float maxY = 5.45f;

    public Vector2 startPos;
    public Vector2 endPos;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    internal void StartStraigth()
    {
        //
        // Random start pos
        float xPosS = 0.0f;
        float yPosS = 0.0f;
        float xPosE = 0.0f;
        float yPosE = 0.0f;
        switch (Random.Range(1, 5))
        {
            case 1:
                xPosS = minX - 2;
                yPosS = (float)Random.Range(-545, 545) / 100.0f;
                xPosE = maxX + 2;
                yPosE = (float)Random.Range(-545, 545) / 100.0f;
                break;
            case 2:
                xPosS = maxX + 2;
                yPosS = (float)Random.Range(-545, 545) / 100.0f;
                xPosE = minX - 2;
                yPosE = (float)Random.Range(-545, 545) / 100.0f;
                break;
            case 3:
                xPosS = (float)Random.Range(-965, 965) / 100.0f;
                yPosS = minY - 2;
                xPosE = (float)Random.Range(-965, 965) / 100.0f;
                yPosE = maxY + 2;
                break;
            case 4:
                xPosS = (float)Random.Range(-965, 965) / 100.0f;
                yPosS = maxY + 2;
                xPosE = (float)Random.Range(-965, 965) / 100.0f;
                yPosE = minY - 2;
                break;
        }
        startPos = new Vector3(xPosS, yPosS, -10.0f);
        endPos = new Vector3(xPosE, yPosE - 10.0f);

        transform.position = startPos;
        currentSpeed = 6.0f;
    }

    // Update is called once per frame
    void Update () {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            updateStraigth();
        }
    }

    void updateStraigth()
    {
        //
        // move from start to end
        Vector3 mvt = new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, -10.0f);
        mvt.Normalize();
        transform.position = new Vector3(transform.position.x + mvt.x * currentSpeed * Time.deltaTime, transform.position.y + mvt.y * currentSpeed * Time.deltaTime, -10.0f);

        //transform.Rotate(Vector3.right * Time.deltaTime * 100.0f);
        transform.Rotate(Vector3.forward * Time.deltaTime * 150.0f);

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
             if (coll.gameObject.tag == "Player")
            {
                GameObject go = GameObject.Find("Game Controller");
                GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
                other.receivEvent(GameControllerScript.EEvent.journeauplus);
                coll.gameObject.SendMessage("Confusing");

                source.PlayOneShot(soundMalus, 1.0f);

                Destroy(this.gameObject);
            }
        }
    }
}