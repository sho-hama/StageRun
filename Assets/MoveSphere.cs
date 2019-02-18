using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour {
    float speed = 10;
    float jump_power = 300;
    bool spaceKeyDown;
    static int MaxJumpTime = 2;
    int JumpAvail = MaxJumpTime;
    // Update is called once per frame
  
    void FixedUpdate() {
      
        float x = Input.GetAxis("Horizontal");
        float y = 0.0f;
        float z = Input.GetAxis("Vertical");

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (spaceKeyDown==true && JumpAvail>0){
            y = 1.0f;
            JumpAvail -= 1;
       
        }
        rigidbody.velocity *= 0.95f;//摩擦による減速 
        rigidbody.AddForce(x * speed, y * jump_power, z * speed);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ spaceKeyDown = true; }
        else { spaceKeyDown = false; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        JumpAvail = MaxJumpTime;
    }
}
