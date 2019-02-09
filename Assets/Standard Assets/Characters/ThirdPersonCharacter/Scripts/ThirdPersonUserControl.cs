using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");//ここで横方向のbool値を取得(左-1　右+1)
            float v = CrossPlatformInputManager.GetAxis("Vertical");//縦方向のbool値を取得(下-1　上+1)
            bool crouch = Input.GetKey(KeyCode.C);//しゃがみモーション

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                //Debug.Log("m_Cam != null\n");
                //カメラの奥方向を正とする。カメラの角度によって4方向の定義を変える。割とこの書き方がテンプレの模様
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right; ;//速度ベクトル
            }
            else
            {
                //Debug.Log("m_Cam == null\n");
                // we use world-relative directions in the case of no main camera
                //カメラがなければ4方向の定義はそのまま座標準拠
                m_Move = v*Vector3.forward + h*Vector3.right;//速度ベクトル(前方向が正, 右方向が正)
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;//左シフトで減速
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);//ここでジャンプモーション, しゃがみモーション, 移動方向のベクトルを入力して実行
            m_Jump = false;
        }
    }
}
