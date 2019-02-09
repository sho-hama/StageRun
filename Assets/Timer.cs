using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static float time;
	// Use this for initialization
	void Start () {
        time = 0;
	}

    // Update is called once per frame
    void Update() {
        if (Goal.goal == false)
        {
            time += Time.deltaTime;
        }
        int t = Mathf.FloorToInt(time);//小数点以下切捨て
        Text uiText = GetComponent<Text>();
        uiText.text = "Time:" + t;//文字連結
	}
}
