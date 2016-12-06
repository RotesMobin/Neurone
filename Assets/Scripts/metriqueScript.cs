using UnityEngine;
using System.Collections;
using System;

public class metriqueScript : MonoBehaviour {

    public float _time { get; set; }
    public float not_move_max { get; set; }
    public float not_move_temp { get; set; }
    public float not_move_total { get; set; }
    public int touch_Objet { get; set; }
    public int touch_Wall {get;set ;}
    public int miss_bonus { get; set; }

    public Metrique m;
	// Use this for initialization
	void Start () {
        m = new Metrique();
        touch_Objet = 0;

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

        string towrite = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text + Environment.NewLine + _time.ToString() + Environment.NewLine + not_move_max.ToString() + Environment.NewLine + not_move_total.ToString() + Environment.NewLine + touch_Objet.ToString() + Environment.NewLine + miss_bonus.ToString();
        m.write_file(towrite);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
