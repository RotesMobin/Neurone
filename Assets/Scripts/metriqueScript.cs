using UnityEngine;
using System.Collections;
using System;

public class metriqueScript : MonoBehaviour {

    public float _time { get; set; }
    public float not_move_max { get; set; }
    public float not_move_temp { get; set; }
    public float not_move_total { get; set; }
    public int touch_Objet { get; set; }
    //public int touch_Wall {get;set ;}
    public int miss_bonus { get; set; }
    public int score;

	// Use this for initialization
	void Start () {
        touch_Objet = 0;
        miss_bonus = 0;
    }
	
	// Update is called once per frame
	void Update () {
        _time += Time.deltaTime;
	if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.RightArrow))
        {
            not_move_max = Mathf.Max(not_move_max, not_move_temp);
            not_move_temp = 0;
        }else
        {
            not_move_total += Time.deltaTime;
            not_move_temp += Time.deltaTime;
        }
	}

    public void write()
    {
        string Line = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text 
            + " " + ((int)(_time * 100)).ToString()
            + " " + ((int)(not_move_total * 100)).ToString()
            + " " + touch_Objet.ToString()
            + " " + miss_bonus.ToString() + " ";
        System.IO.File.AppendAllText(@"Assets\Scripts\File\fann.data", Line);// WriteAllText(@"Assets\Scripts\File\lol.txt", Line);
    }

    public void callFann()
    {
        Debug.Log(Application.dataPath);
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = Application.dataPath + "/NeuralNetwork";
        proc.StartInfo.Arguments = "\"" + Application.dataPath + "/network.net\" "
            + GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text + " "
            + ((int)(_time*100)).ToString() + " "
            + ((int)(not_move_total * 100)).ToString() + " "
            + touch_Objet.ToString() + " "
            + miss_bonus.ToString() + " ";

        proc.Start();
        proc.WaitForExit();
        Debug.Log("Exit code: " + proc.ExitCode);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
