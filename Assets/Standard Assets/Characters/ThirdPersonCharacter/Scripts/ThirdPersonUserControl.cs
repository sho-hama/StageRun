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
            float h = CrossPlatformInputManager.GetAxis("Horizontal");//�����ŉ�������bool�l���擾(��-1�@�E+1)
            float v = CrossPlatformInputManager.GetAxis("Vertical");//�c������bool�l���擾(��-1�@��+1)
            bool crouch = Input.GetKey(KeyCode.C);//���Ⴊ�݃��[�V����

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                //Debug.Log("m_Cam != null\n");
                //�J�����̉������𐳂Ƃ���B�J�����̊p�x�ɂ����4�����̒�`��ς���B���Ƃ��̏��������e���v���̖͗l
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right; ;//���x�x�N�g��
            }
            else
            {
                //Debug.Log("m_Cam == null\n");
                // we use world-relative directions in the case of no main camera
                //�J�������Ȃ����4�����̒�`�͂��̂܂܍��W����
                m_Move = v*Vector3.forward + h*Vector3.right;//���x�x�N�g��(�O��������, �E��������)
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;//���V�t�g�Ō���
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);//�����ŃW�����v���[�V����, ���Ⴊ�݃��[�V����, �ړ������̃x�N�g������͂��Ď��s
            m_Jump = false;
        }
    }
}
