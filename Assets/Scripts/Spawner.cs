using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private float m_FuckingWaitSeconds;
    [SerializeField] private GameObject[] m_FuckingEnemies;

    #endregion

    #region Start

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    #endregion

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(m_FuckingWaitSeconds);

        int enemy = Random.Range(0, m_FuckingEnemies.Length);
        // Spawn
        StartCoroutine(Spawn());
    }
}
