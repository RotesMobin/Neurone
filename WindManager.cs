using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WindManager : MonoBehaviour {
    public AudioClip soundWind;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    List<GameObject> trail = new List<GameObject>();
    List<GameObject> trailOff = new List<GameObject>();
    int nbTrail;
    int nbTrailActivated;
    float deltaTrail;
    public GameObject WindTrailPrefab;

    PlayerController player;

    EWindBehaviour      m_currentBehaviour;
    EWindState          m_currentState;

    float               currentMvt;
    public float        deltaMvtTrail;
    public float        currentSpeed;

    float currentTime;

    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 endPosMiddle;

    float minX = -9.65f;
    float maxX = 9.65f;
    float minY = -5.45f;
    float maxY = 5.45f;

    enum EWindBehaviour
    {
        e_behaviour_division,
        e_behaviour_straigth,
        e_behaviour_speed_change,
        e_behaviour_direction_change
    }

    enum EWindState
    {
        e_state_moving,
        e_state_exiting,
        e_state_starting
    }

    internal void StartStraigth()
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
                yPosS = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                xPosE = maxX + 4;
                yPosE = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                break;
            case 2:
                xPosS = maxX + 2;
                yPosS = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                xPosE =  minX - 4;
                yPosE = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                break;
            case 3:
                xPosS = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosS = minY - 2;
                xPosE = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosE = maxY + 4;
                break;
            case 4:
                xPosS = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosS = maxY + 2;
                xPosE = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosE = minY - 4;
                break;
        }
        startPos = new Vector3(xPosS, yPosS, -20);
        endPos = new Vector3(xPosE, yPosE, -20);

        transform.position = startPos;

        m_currentBehaviour = EWindBehaviour.e_behaviour_straigth;
        m_currentState = EWindState.e_state_moving;
        currentSpeed = 7.5f;
    }

    internal void StartDirection()
    {
        //
        // Random start pos
        float xPosS = 0.0f;
        float yPosS = 0.0f;
        float xPosE = 0.0f;
        float yPosE = 0.0f;
        float xPosM = 0.0f;
        float yPosM = 0.0f;
        switch (UnityEngine.Random.Range(1, 5))
        {
            case 1:
                xPosS = minX - 2;
                yPosS = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                xPosE = maxX + 4;
                yPosE = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                switch (UnityEngine.Random.Range(1, 3))
                {
                    case 1:
                        xPosM = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                        yPosM = minY - 4;
                        break;
                    case 2:
                        xPosM = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                        yPosM = maxY + 4;
                        break;
                }
                break;
            case 2:
                xPosS = maxX + 2;
                yPosS = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                xPosE = minX - 4;
                yPosE = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                switch (UnityEngine.Random.Range(1, 3))
                {
                    case 1:
                        xPosM = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                        yPosM = minY - 4;
                        break;
                    case 2:
                        xPosM = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                        yPosM = maxY + 4;
                        break;
                }
                break;
            case 3:
                xPosS = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosS = minY - 2;
                xPosE = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosE = maxY + 4;
                switch (UnityEngine.Random.Range(1, 3))
                {
                    case 1:
                        xPosM = minX - 4; 
                        yPosM = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                        break;
                    case 2:
                        xPosM = maxX + 4;
                        yPosM = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                        break;
                }
                break;
            case 4:
                xPosS = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosS = maxY + 2;
                xPosE = (float)UnityEngine.Random.Range(-965, 965) / 100.0f;
                yPosE = minY - 4;
                switch (UnityEngine.Random.Range(1, 3))
                {
                    case 1:
                        xPosM = minX - 4;
                        yPosM = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                        break;
                    case 2:
                        xPosM = maxX + 4;
                        yPosM = (float)UnityEngine.Random.Range(-545, 545) / 100.0f;
                        break;
                }
                break;
        }
        startPos = new Vector3(xPosS, yPosS, -20);
        endPos = new Vector3(xPosE, yPosE, -20);
        endPosMiddle = new Vector3(xPosM, yPosM, -20);

        transform.position = startPos;

        m_currentBehaviour = EWindBehaviour.e_behaviour_direction_change;
        m_currentState = EWindState.e_state_moving;
        currentSpeed = 7.5f;
        currentTime = 0.0f;
    }

    void OnEnable ()
    {
        m_currentBehaviour = EWindBehaviour.e_behaviour_straigth;
        m_currentState = EWindState.e_state_starting;
        currentMvt = 0.0f;
        nbTrail = 0;
        nbTrailActivated = 0;
        currentSpeed = 7.5f;
        startPos = new Vector3(0.0f, 0.0f, -20);
        endPos = new Vector3(0.0f, 0.0f, -20);
        deltaTrail = 0.0f;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            switch (m_currentState)
            {
                case EWindState.e_state_moving:
                    switch (m_currentBehaviour)
                    {
                        case EWindBehaviour.e_behaviour_division:
                            updateDivision();
                            break;
                        case EWindBehaviour.e_behaviour_straigth:
                            updateStraigth();
                            break;
                        case EWindBehaviour.e_behaviour_speed_change:
                            updateSpeed();
                            break;
                        case EWindBehaviour.e_behaviour_direction_change:
                            updateDirection();
                            break;
                    }
                    break;
            }
            //player = GetComponent<PlayerController>();

            updateTrail();

            updateAnimation();
        }
	}

    void updateTrail()
    {   
        switch(m_currentState)
        {
            case EWindState.e_state_starting:
                break;
            case EWindState.e_state_exiting:
                break;
            case EWindState.e_state_moving:
                deltaTrail += Time.deltaTime;
                if (deltaTrail > deltaMvtTrail)
                {
                    //
                    // TODO add prefab
                    if(trailOff.Count > 0)
                    {
                        trailOff[0].SetActive(true);
                        trailOff[0].GetComponent<WindTrail>().launch(transform.position);
                        trail.Add(trailOff[0]);
                        trailOff.Remove(trailOff[0]);
                        //
                        // TODO activate
                    }
                    else
                    {
                        GameObject tmp = (GameObject)Instantiate(WindTrailPrefab);

                        trail.Add(tmp);
                        tmp.SetActive(true);
                        tmp.GetComponent<WindTrail>().launch(transform.position);
                    }
                    deltaTrail = 0.0f;
                }
                break;
        }

        for(int nTrail = trail.Count - 1; nTrail >= 0; --nTrail)
        {
            if(trail[nTrail].GetComponent<WindTrail>().isOver() == true)
            {
                trailOff.Add(trail[nTrail]);
                trail[nTrail].SetActive(false);
                trail.Remove(trail[nTrail]);
            }
        }
    }

    void updateAnimation()
    {
    }

    void updateDivision()
    {
    }

    void updateStraigth()
    {
        //
        // move from start to end

        Vector3 mvt = new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, -20);
        mvt.Normalize();
        transform.position = new Vector3(transform.position.x + mvt.x * currentSpeed * Time.deltaTime, transform.position.y + mvt.y * currentSpeed * Time.deltaTime, -20);

        //
        // Check if we are out of screen

        if (transform.position.x < minX - 2
            || transform.position.y < minY - 2
            || transform.position.x > maxX + 2
            || transform.position.y > maxY + 2)
        {
            //transform.position = startPos;
            this.gameObject.SetActive(false);
        }
    }

    void updateSpeed()
    {
    }

    void updateDirection()
    {
        currentTime += Time.deltaTime;
        Vector3 mvt;
        if (currentTime > 1.0f)
        {
            mvt = new Vector3(endPosMiddle.x - transform.position.x, endPosMiddle.y - transform.position.y, -5);
        }
        else
        {
            mvt = new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, -5);
        }
        //
        // move from start to end
        mvt.Normalize();
        transform.position = new Vector3(transform.position.x + mvt.x * currentSpeed * Time.deltaTime, transform.position.y + mvt.y * currentSpeed * Time.deltaTime, -20);

        //
        // Check if we are out of screen

        if (transform.position.x < minX - 2
            || transform.position.y < minY - 2
            || transform.position.x > maxX + 2
            || transform.position.y > maxY + 2)
        {
            //transform.position = startPos;
            this.gameObject.SetActive(false);
        }
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
