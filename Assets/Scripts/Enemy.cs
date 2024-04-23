using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables

    private int m_Direction;
    [SerializeField] private Vector2 m_SpeedRange;
    private float m_Speed;

    #endregion

    #region Start

    private void Start()
    {
        // Randomize speed
        m_Speed = Random.Range(m_SpeedRange.x, m_SpeedRange.y);

        // Choose direction to move depending on which side the enemy is spawned
        if (transform.position.x > 0)
        {
            m_Direction = -1;
        }
        else
        {
            m_Direction = 1;
        }

        Destroy(gameObject, 7);
    }

    #endregion

    #region Update

    private void Update()
    {
        // Enemy movement
        transform.position += Vector3.right * m_Speed * m_Direction * Time.deltaTime;
    }

    #endregion
}
