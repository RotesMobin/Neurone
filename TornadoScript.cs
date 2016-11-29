using UnityEngine;
using System.Collections;

public class TornadoScript : MonoBehaviour {
    bool keepPlayer;

    PlayerController player;

    public float currentSpeed;

    float minX = -9.65f;
    float maxX = 9.65f;
    float minY = -5.45f;
    float maxY = 5.45f;

    public Vector2 startPos;
    public Vector2 endPos;
    GameControllerScript ctrl;

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
        startPos = new Vector2(xPosS, yPosS);
        endPos = new Vector2(xPosE, yPosE);

        transform.position = startPos;
        currentSpeed = 2.0f;
    }

    internal void StartDirection()
    {
        //
        // Random start pos
        float xPosS = 0.0f;
        float yPosS = 0.0f;
        float xPosE = 0.0f;
        float yPosE = 0.0f;
        switch (UnityEngine.Random.Range(1, 5))
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
        startPos = new Vector2(xPosS, yPosS);
        endPos = new Vector2(xPosE, yPosE);

        transform.position = startPos;
        currentSpeed = 2.0f;
    }

    // Use this for initialization
    void Start () {
        keepPlayer = false;
        player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
	}

    void updateStraigth()
    {
        //
        // move from start to end

        Vector3 mvt = new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, 0);
        mvt.Normalize();
        transform.Translate(mvt * currentSpeed * Time.deltaTime);

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

    void updateDirection()
    {

        Vector3 mvt;
        mvt = new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, 0);
        //
        // move from start to end
        mvt.Normalize();
        transform.Translate(mvt * currentSpeed * Time.deltaTime);

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

    // Update is called once per frame
    void Update () {
        updateStraigth();
        updateDirection();
    }

void OnCollisionEnter2D(Collision2D coll)
    {
        if (player.isProtected == false)
        {
            if (coll.gameObject.tag == "Player")
            {
                coll.gameObject.SendMessage("Kidnapped", this.gameObject);
                GameObject go = GameObject.Find("Game Controller");
                GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
                other.receivEvent(GameControllerScript.EEvent.tourbillionplus);
            }
        }
    }
}