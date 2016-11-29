using UnityEngine;
using System.Collections;

public abstract class GameMissions : MonoBehaviour {

    public float    timeMax;
    public int      score;

    float           m_currentTime;
    EMissionState   m_currentState;

    public enum EMissionState
    {
        e_state_begining,
        e_state_ending,
        e_state_playing
    }
    
    // Use this for initialization
	void Start () {
        m_currentState = EMissionState.e_state_begining;
        m_currentTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        switch (m_currentState)
        {
            case EMissionState.e_state_begining:
                break;
            case EMissionState.e_state_playing:
                m_currentTime += Time.deltaTime;
                if (m_currentTime > timeMax)
                {
                    //
                    // Go on end state
                    m_currentState = EMissionState.e_state_ending;
                }
                else
                {

                }
                break;
            case EMissionState.e_state_ending:
                break;
        }
    }
    public abstract EMissionState startMission();
    public abstract EMissionState updateMission();
    public abstract EMissionState finishMission();
}
