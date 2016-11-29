using UnityEngine;
using System.Collections;
using System;

public class CollectiblesSpawning : MonoBehaviour
{
    GameObject SP1;
    GameObject SP2;
    GameObject SP3;

    public GameObject plush;
    public GameObject miku;
    public GameObject nintendo;
    public GameObject yaoi;
    public GameObject food;
    public GameObject umbrella;

    GameObject collectable;

    float m_gameTime;

    int m_randomPosition;
    float m_randomPrefab;

    Vector3 spawnPosition;

    bool p1Free = true;
    bool p2Free = true;
    bool p3Free = true;

    // Use this for initialization
    void Start()
    {
        SP1 = GameObject.Find("FirstSpawn");
        SP2 = GameObject.Find("SecondSpawn");
        SP3 = GameObject.Find("ThirdSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            m_gameTime += Time.deltaTime;

            if (m_gameTime >= 2 && (p1Free == true || p2Free == true || p3Free == true))
            {
                m_randomPosition = UnityEngine.Random.Range(1, 4);
                m_randomPrefab = UnityEngine.Random.Range(0.0f, 4.4f);

                if (m_randomPosition == 1 && p1Free == true)
                {
                    spawnPosition = SP1.transform.position;
                    p1Free = false;
                }
                else if (m_randomPosition == 2 && p2Free == true)
                {
                    spawnPosition = SP2.transform.position;
                    p2Free = false;
                }
                else if (m_randomPosition == 3 && p3Free == true)
                {
                    spawnPosition = SP3.transform.position;
                    p3Free = false;
                }
                else if (p3Free == true)
                {
                    m_randomPosition = 3;
                    spawnPosition = SP3.transform.position;
                    p3Free = false;
                }
                else if (p2Free == true)
                {
                    m_randomPosition = 2;
                    spawnPosition = SP2.transform.position;
                    p2Free = false;
                }
                else if (p1Free == true)
                {
                    m_randomPosition = 1;
                    spawnPosition = SP1.transform.position;
                    p1Free = false;
                }

                if (m_randomPrefab <= 0.5f)
                {
                    collectable = miku;
                }
                else if ((m_randomPrefab <= 3f) && (m_randomPrefab > 0.5f))
                {
                    collectable = plush;
                }
                else if ((m_randomPrefab <= 3.5f) && (m_randomPrefab > 3f))
                {
                    collectable = nintendo;
                }
                else if ((m_randomPrefab <= 4.0f) && (m_randomPrefab > 3.5f))
                {
                    collectable = yaoi;
                }
                else if ((m_randomPrefab <= 4.2f) && (m_randomPrefab > 4.0f))
                {
                    collectable = food;
                }
                else if ((m_randomPrefab <= 4.4f) && (m_randomPrefab > 4.2f))
                {
                    collectable = umbrella;
                }

                GameObject collectible = Instantiate(collectable, spawnPosition, Quaternion.identity) as GameObject;
                if (collectable == umbrella)
                {
                    collectible.GetComponent<UmbrellaScript>().addPos(m_randomPosition);
                }
                else if (collectable == food)
                {
                    collectible.GetComponent<RamenScript>().addPos(m_randomPosition);
                }
                else
                {
                    collectible.GetComponent<CollectibleScript>().addPos(m_randomPosition);
                    collectible.GetComponent<CollectibleScript>().setType(m_randomPrefab);
                }
                m_gameTime = 0f;
            }
        }
    }

    public void setFree(int nb)
    {
        switch (nb)
        {
            case 1:
                p1Free = true;
                break;
            case 2:
                p2Free = true;
                break;

            case 3:
                p3Free = true;
                break;
        }
    }
}
