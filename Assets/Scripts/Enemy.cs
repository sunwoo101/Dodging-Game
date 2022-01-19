using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables

    private int m_FuckingDirection;
    [SerializeField] private float m_FuckingSpeed;

    #endregion

    #region Start

    private void Start()
    {
        if (transform.position.x > 0)
        {
            m_FuckingDirection = -1;
        }
        else
        {
            m_FuckingDirection = 1;
        }

        Destroy(gameObject, 7);
    }

    #endregion

    #region Update

    private void Update()
    {
        transform.position += Vector3.right * m_FuckingSpeed * m_FuckingDirection * Time.deltaTime;
    }

    #endregion
}
