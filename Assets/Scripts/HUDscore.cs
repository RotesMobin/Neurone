using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDscore : MonoBehaviour
{
    public static int score;

    Text text;
    float time;
    public float timeUpdate;
    public int scoreUpdate;

    //Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        time = 0;
        text.text = "" + 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        if (!other.isOver())
        {
            time += Time.deltaTime;
            if (time > timeUpdate)
            {
                time = 0.0f;
                score += scoreUpdate;
                text.text = "" + score;
            }
        }
    }

    public void AddScore(int nb)
    {
        score += nb;
    }
}
