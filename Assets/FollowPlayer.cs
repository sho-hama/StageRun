using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; //ターゲットへの参照
    private Vector3 offset; //相対座標
    void Start()
    {
        //カメラ自身と対象物との相対距離を求める
        offset = GetComponent<Transform>().position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = target.position + offset;
    }
}
