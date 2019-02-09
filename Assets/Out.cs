using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Out : MonoBehaviour {
    private void OnTriggerEnter(Collider col) { //別のオブジェクトが触れた瞬間に実行される予約関数
        if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);//現在のシーンを再読み込みする。ゲーム中はmainシーンなのでmainシーンを再読み込みする
        }
    }
}
