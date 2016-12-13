using UnityEngine;
using System.Collections;
using System;

using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public AudioClip gameMusic;
    private AudioSource source;

    public float m_gameScore;
    public GameObject WindPrefab;
    public GameObject TornadoPrefab;
    public GameObject TrashPrefab;
    public GameObject PaperPrefab;
    bool m_isOver;

    GameObject trash;
    GameObject paper;

    public GameObject[] winds;
    public GameObject[] tornados;
    string[] missionText = { "Collect only the CAT", "Collect only the HENTAI", "Get Hit by THRASH", "Get Hit by the TORNADO", "Get Hit by the JOURNAL", "Avoid EVERYTHING" };
    GameObject[] texts;

    public float deltaTimeWind;
    public float deltaTimeTornado;
    public float deltaTimeMalus;
    float m_currentTime;
    float m_currentTimeWind;
    float m_currentTimeMalus;

    float m_currentTimeTornado;
    EMission m_currentMission;

    public float m_deltaTimeMissions;
    float m_timeMission;
    public bool m_currentMissionFailed;

    float m_timeCurrentMission;

    public float m_timeToDoMission;

    GameObject HUDDefis;

    //Text text;
    Text m_missionText;
    GameObject spriteDefis;
    float m_timeCurrentMissionEsq;
    //
    // Missions
    int nbChat;
    int nbHentai;
    int nbPoubelle;
    int nbTourbillion;
    int nbJourneaux;
    int test = 1;
    enum EMission
    {
        comboChat,
        comboHentai,
        comboPoubelle,
        comboJournaux,
        esquive,
        none,
        comboTourbillion
    }

    public enum EEvent
    {
        chatplus,
        hentaiplus,
        poubelleplus,
        tourbillionplus,
        journeauplus,
        autreEvent
    }
    // Use this for initialization
    void Start()
    {
        m_currentMissionFailed = false;
        HUDDefis = GameObject.Find("HUD_Defis");
        Color tmp = HUDDefis.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        HUDDefis.GetComponent<SpriteRenderer>().color = tmp;

        //m_missionText = GameObject.Find("missionText");
        //m_missionText.GetComponent<TextMesh>().text = "TEST";
        m_missionText = GameObject.Find("missionText").GetComponent<Text>(); ;
        //m_missionText.text = "TEST 22222";

        /*tmp = m_missionText.GetComponent<TextMesh>().color;
        tmp.a = 0.0f;
        m_missionText.GetComponent<TextMesh>().color = tmp;*/

        winds = new GameObject[10];
        for (int i = 0; i < winds.Length; i++)
        {
            winds[i] = (GameObject)Instantiate(WindPrefab);
            winds[i].SetActive(false);
        }

        tornados = new GameObject[10];
        for (int i = 0; i < tornados.Length; i++)
        {
            tornados[i] = (GameObject)Instantiate(TornadoPrefab);
            tornados[i].SetActive(false);
        }


        m_currentTime = 0.0f;
        m_timeMission = 0.0f;

        tmp = GameObject.Find("HUD_Restart").GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        GameObject.Find("HUD_Restart").GetComponent<SpriteRenderer>().color = tmp;

        //
        // Find text object
        texts = new GameObject[10];
        texts[0] = GameObject.FindWithTag("Text_1");
        texts[1] = GameObject.Find("Text_2");
        texts[2] = GameObject.Find("Text_3");
        texts[3] = GameObject.Find("Text_4");
        texts[4] = GameObject.Find("Text_5");
        texts[5] = GameObject.Find("Text_6");
        texts[6] = GameObject.Find("Text_7");
        texts[7] = GameObject.Find("Text_8");
        texts[8] = GameObject.Find("Text_9");
        texts[9] = GameObject.Find("Text_10");

        texts[0].SetActive(false);
        texts[1].SetActive(false);
        texts[2].SetActive(false);
        texts[3].SetActive(false);
        texts[4].SetActive(false);
        texts[5].SetActive(false);
        texts[6].SetActive(false);
        texts[7].SetActive(false);
        texts[8].SetActive(false);
        texts[9].SetActive(false);
        //GameObject.Find("Text_1").SetActive(false);
        m_currentMission = EMission.none;

        spriteDefis = GameObject.Find("SpriteDefis");
        tmp = spriteDefis.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        spriteDefis.GetComponent<SpriteRenderer>().color = tmp;

        source.Play();
    }

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (m_isOver)
        {

        }
        else
        {
            //
            // Update Shadow
            GameObject o1 = GameObject.Find("ombre1");
            GameObject o2 = GameObject.Find("ombre2");

            o1.transform.position = new Vector3(o1.transform.position.x + 2 * Time.deltaTime, o1.transform.position.y, o1.transform.position.z);
            o2.transform.position = new Vector3(o2.transform.position.x + 2 * Time.deltaTime, o2.transform.position.y, o2.transform.position.z);
            if (o1.transform.position.x > 40.0f)
            {
                o1.transform.position = new Vector3(-60.0f + (o1.transform.position.x - 40.0f), o1.transform.position.y, o1.transform.position.z); ;
            }
            if (o2.transform.position.x > 40.0f)
            {
                o2.transform.position = new Vector3(-60.0f + (o2.transform.position.x - 40.0f), o2.transform.position.y, o2.transform.position.z); ;
            }

            if (test == 1)
            {

            }
            m_currentTime += Time.deltaTime;
            m_currentTimeWind += Time.deltaTime;
            m_currentTimeTornado += Time.deltaTime;
            m_currentTimeMalus += Time.deltaTime;

            //
            // Wind or ennemys apparition
            if (m_currentTimeWind > deltaTimeWind)
            {
                //
                // found a free wind
                int currentWind = -1;
                for (int i = 0; i < winds.Length; i++)
                {
                    if (winds[i].activeInHierarchy == false)
                    {
                        currentWind = i;
                        break;
                    }
                }
                if (currentWind != -1)
                {
                    winds[currentWind].SetActive(true);

                    switch (UnityEngine.Random.Range(1, 3))
                    {
                        case 1:
                            winds[currentWind].GetComponent<WindManager>().StartStraigth();

                            break;

                        case 2:
                            winds[currentWind].GetComponent<WindManager>().StartDirection();
                            break;
                    }
                }

                m_currentTimeWind = 0.0f;
            }

            if (m_currentTimeMalus > deltaTimeMalus)
            {

                switch (UnityEngine.Random.Range(2, 4))
                {
                    case 1:
                    /*tornado = (GameObject)Instantiate(TornadoPrefab);
                    tornado.GetComponent<TornadoScript>().StartStraigth();
                    break;*/

                    case 2:
                        trash = (GameObject)Instantiate(TrashPrefab);
                        trash.GetComponent<TrashScript>().StartStraigth();
                        break;

                    case 3:
                        paper = (GameObject)Instantiate(PaperPrefab);
                        paper.GetComponent<PaperScript>().StartStraigth();
                        break;
                }

                m_currentTimeMalus = 0.0f;
            }

            //
            // Missions
            updateMission();
        }
    }

    private void updateMission()
    {
        if (m_currentMission == EMission.none)
        {
            m_timeMission += Time.deltaTime;
            if (m_timeMission > m_deltaTimeMissions)
            {
                m_timeMission = 0.0f;
                //
                // Choose a new mission

                // TODO TEST
                m_currentMission = (EMission)UnityEngine.Random.Range(0, 5);

                //m_currentMission = EMission.esquive;

                nbChat = 0;
                nbHentai = 0;
                nbPoubelle = 0;
                nbTourbillion = 0;
                nbJourneaux = 0;
                m_currentMissionFailed = false;
                Color tmp = HUDDefis.GetComponent<SpriteRenderer>().color;
                tmp.a = 1.0f;
                HUDDefis.GetComponent<SpriteRenderer>().color = tmp;
                m_timeCurrentMission = 0.0f;

                tmp = spriteDefis.GetComponent<SpriteRenderer>().color;
                tmp.a = 1.0f;
                spriteDefis.GetComponent<SpriteRenderer>().color = tmp;

                Sprite sprite;

                switch (m_currentMission)
                {
                    case EMission.comboChat:
                        m_missionText.text = "Get 5 CAT";
                        sprite = Resources.Load<Sprite>("PourILLU_Boys");
                        spriteDefis.GetComponent<SpriteRenderer>().sprite = sprite;
                        break;
                    case EMission.comboHentai:
                        m_missionText.text = "Get 3 YAOI";
                        sprite = Resources.Load<Sprite>("PourILLU_Ombr");
                        spriteDefis.GetComponent<SpriteRenderer>().sprite = sprite;
                        break;
                    case EMission.comboJournaux:
                        m_missionText.text = "HIT 2 PAPER";
                        sprite = Resources.Load<Sprite>("PourILLU_Switch");
                        spriteDefis.GetComponent<SpriteRenderer>().sprite = sprite;
                        break;
                    case EMission.comboPoubelle:
                        m_missionText.text = "HIT 4 TRASH";
                        sprite = Resources.Load<Sprite>("PourILLU_Assune");
                        spriteDefis.GetComponent<SpriteRenderer>().sprite = sprite;
                        break;
                    case EMission.comboTourbillion:
                        m_missionText.text = "HIT 3 TORNADO";
                        sprite = Resources.Load<Sprite>("PourILLU_Boys");
                        spriteDefis.GetComponent<SpriteRenderer>().sprite = sprite;
                        break;
                    case EMission.esquive:
                        m_missionText.text = "Avoid everything";
                        sprite = Resources.Load<Sprite>("PourILLU_Boys");
                        spriteDefis.GetComponent<SpriteRenderer>().sprite = sprite;
                        break;
                }
                m_timeCurrentMissionEsq = 0.0f;
            }
        }
        else if (m_currentMission == EMission.esquive)
        {
            m_timeCurrentMissionEsq += Time.deltaTime;
            if (m_timeCurrentMissionEsq > 10.0f)
            {
                missionOK(2000);
                m_timeCurrentMissionEsq = 0.0f;
            }
        }

        //
        // Update Mission time
        if (m_currentMission != EMission.none)
        {
            m_timeCurrentMission += Time.deltaTime;
            float timeLeft = m_timeToDoMission - m_timeCurrentMission;
            if (timeLeft < 0.0f)
            {
                timeLeft = 0;
                missionKO();
            }
        } 
    }

    public void receivEvent(EEvent evt)
    {
        switch (evt)
        {
            case EEvent.chatplus:
                if (m_currentMission == EMission.comboChat)
                {
                    nbChat++;
                    if (nbChat >= 5)
                    {
                        missionOK(650);
                    }
                }
                else if (m_currentMission != EMission.none)
                {
                    missionKO();
                }
                break;
            case EEvent.hentaiplus:
                if (m_currentMission == EMission.comboHentai)
                {
                    nbHentai++;
                    if (nbHentai >= 3)
                    {
                        missionOK(6000);
                    }
                }
                else if (m_currentMission != EMission.none)
                {
                    missionKO();
                }
                break;
            case EEvent.poubelleplus:
                if (m_currentMission == EMission.comboPoubelle)
                {
                    nbPoubelle++;
                    if (nbPoubelle >= 4)
                    {
                        missionOK(600);
                    }
                }
                else if (m_currentMission != EMission.none)
                {
                    missionKO();
                }
                break;
            case EEvent.tourbillionplus:
                if (m_currentMission == EMission.comboTourbillion)
                {
                    nbTourbillion++;
                    if (nbTourbillion >= 3)
                    {
                        missionOK(350);
                    }
                }
                else if (m_currentMission != EMission.none)
                {
                    missionKO();
                }
                break;
            case EEvent.journeauplus:
                if (m_currentMission == EMission.comboJournaux)
                {
                    nbJourneaux++;
                    if (nbJourneaux >= 2)
                    {
                        missionOK(200);
                    }
                }
                else if (m_currentMission != EMission.none)
                {
                    missionKO();
                }
                break;
            case EEvent.autreEvent:
                if (m_currentMission != EMission.none)
                {
                    missionKO();
                }
                break;
        }
    }

    private void missionKO()
    {
        m_currentMission = EMission.none;
        Color tmp = HUDDefis.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        HUDDefis.GetComponent<SpriteRenderer>().color = tmp;
        printScore(new Vector2(HUDDefis.transform.position.x, HUDDefis.transform.position.y - 0.5f), "FAILED =(");

        tmp = spriteDefis.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        spriteDefis.GetComponent<SpriteRenderer>().color = tmp;
        
        m_missionText.text = "";
    }

    private void missionOK(int nbPoint)
    {
        m_currentMission = EMission.none;
        Color tmp = HUDDefis.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        HUDDefis.GetComponent<SpriteRenderer>().color = tmp;
        printScore(new Vector2(HUDDefis.transform.position.x, HUDDefis.transform.position.y - 0.5f), "COMPLETED +" + nbPoint.ToString());

        tmp = spriteDefis.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        spriteDefis.GetComponent<SpriteRenderer>().color = tmp;

        m_missionText.text = "";


        GameObject go = GameObject.Find("Text");
        HUDscore hud = (HUDscore)go.GetComponent<HUDscore>();
        hud.AddScore(nbPoint);
    }

    public void printScore(Vector2 position, string v)
    {
        //
        // Find score to use
        int currentTxt = -1;
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].activeSelf == false)
            {
                currentTxt = i;
                break;
            }
        }
        if (currentTxt != -1)
        {
            //
            // Change his data
            texts[currentTxt].SetActive(true);
            texts[currentTxt].GetComponent<ScoreScript>().StartAff(position, v);
        }
    }

    public void setStateOver()
    {
        m_isOver = true;
    }

    public bool isOver()
    {
        return m_isOver;
    }
}
