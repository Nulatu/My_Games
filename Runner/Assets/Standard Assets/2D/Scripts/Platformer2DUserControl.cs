using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Down;
        float koordinat_ground;
        float max_visota_jump;
        float max_douwn_jump;

        bool block_knopki; // ����� �� �������� ��������� ��� ������ ������,�����(��-�� ����� �������� � ����������� ��������)
        float time_block_knopki;
        public float  timing_megdu_knopkami;

        private void Awake()
        {
            koordinat_ground = gameObject.transform.position.y;
            max_visota_jump = koordinat_ground + 2.3f;
            max_douwn_jump = koordinat_ground - 2.1f;
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump && block_knopki == false)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump"); // ����������� ������ �� ���� ������� Jump,���� ������ �� ���������� true � m_Jump
                //��� ���� ������ � ������� ButtonHandler
                if (m_Jump == true)
                {
                    time_block_knopki = timing_megdu_knopkami;
                    block_knopki = true;
                }
            }
            if (!m_Down && block_knopki == false)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Down = CrossPlatformInputManager.GetButtonDown("Down"); // ����������� ������ �� ���� ������� Down,���� ������ �� ���������� true � m_Down
                //��� ���� ������ � ������� ButtonHandler
                if (m_Down == true)
                {
                    time_block_knopki = timing_megdu_knopkami;
                    block_knopki = true;
                }
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            //bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Vertical");
            if (h > 0.2f && block_knopki == false)
            {
                m_Jump = true;
                time_block_knopki = timing_megdu_knopkami;
                block_knopki = true;
                h = 0;
            }
            else if(h< - 0.2f && block_knopki == false) // ���� ������� <0.2 �������� 0 �� ����� �� ������� ������ �������� ����� �� ����� ����� �������� ���� �� �������� � ��� ������ �� �� ������ ������
            {
                m_Down = true;
                time_block_knopki = timing_megdu_knopkami;
                block_knopki = true;
                h = 0;
            }

            // Pass all parameters to the character control script.

            time_block_knopki -= Time.deltaTime;
            if (time_block_knopki <= 0)
                block_knopki = false;

            m_Character.Move(0, m_Jump, m_Down);
            m_Jump = false;// ����� ������� ����� ������� 
            m_Down = false;// ����� ������� ����� ������� 


        }
    }
}
