using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveX : MonoBehaviour
{
    [SerializeField]
    public float m_Velocity = 10;

    [SerializeField]
    public Transform m_LimitRight;
    [SerializeField]
    public Transform m_LimitLeft;

    FloatingJoystick m_Joystick;
    Rigidbody2D m_PlayerRB;
    void Start()
    {
        m_Joystick = FindObjectOfType<FloatingJoystick>();
        m_PlayerRB = GetComponent<Rigidbody2D>();

        m_Velocity = Screen.width * m_Velocity / 1280f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FallFoodController.Instance.GameOver)
        {
            m_PlayerRB.velocity = new Vector2(0, 0);
            return;
        }

        float h = m_Joystick.Horizontal;

        m_PlayerRB.velocity = new Vector2(h * m_Velocity, 0);

        Vector3 tempPos = transform.position;

        if(transform.position.x > m_LimitRight.position.x)
        {
            tempPos.x = m_LimitRight.position.x;
        } else if (transform.position.x < m_LimitLeft.position.x)
        {
            tempPos.x = m_LimitLeft.position.x;

        }
        transform.position = tempPos;

    }
}
