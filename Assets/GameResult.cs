using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour {

    // Use this for initialization
    private int highScore;
    public Text resultTime;
    public Text bestTime;
    public GameObject resultUI;
	void Start () {
        if (PlayerPrefs.HasKey("HighScore")){//HighScoreというセーブデータが存在するかどうか
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else{
            highScore = 999;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Goal.goal){
            resultUI.SetActive(true);//ここで結果の表示を行う
            int result = Mathf.FloorToInt(Timer.time);
            resultTime.text = "ResultTime:" + result;
            bestTime.text = "BestTime:" + highScore;

            if (highScore > result){
                PlayerPrefs.SetInt("HighScore", result);//HighScoreというセーブデータにハイスコアの結果を記録
            }
        }
    }	
    public void OnRetry(){
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }

    public void OnTitle(){
        SceneManager.LoadScene("Title");
    }

}
