using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private Vector2 m_FuckingWaitSecondsRange;
    private float m_FuckingWaitSeconds;
    [SerializeField] private GameObject[] m_FuckingEnemies;
    [SerializeField] private Transform m_FuckingYMax;
    [SerializeField] private Transform m_FuckingYMin;

    #endregion

    #region Start

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    #endregion

    #region Spawn

    IEnumerator Spawn()
    {
        m_FuckingWaitSeconds = Random.Range(m_FuckingWaitSecondsRange.x, m_FuckingWaitSecondsRange.y);

        yield return new WaitForSeconds(m_FuckingWaitSeconds);

        int enemyIndex = Random.Range(0, m_FuckingEnemies.Length);
        float yPos = Random.Range(m_FuckingYMin.position.y, m_FuckingYMax.position.y);

        Instantiate(m_FuckingEnemies[enemyIndex], new Vector3(transform.position.x, yPos, transform.position.z), Quaternion.identity);

        // Spawn
        StartCoroutine(Spawn());
    }

    #endregion
}
