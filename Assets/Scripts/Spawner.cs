using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private Vector2 m_WaitSecondsRange;
    private float m_WaitSeconds;
    [SerializeField] private GameObject[] m_Enemies; // List of enemy prefabs that can be spawned
    [SerializeField] private Transform m_YMax; // Highest spawn point
    [SerializeField] private Transform m_YMin; // Lowest spawn point

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
        // Generate a random number of seconds to wait before spawning the enemy
        m_WaitSeconds = Random.Range(m_WaitSecondsRange.x, m_WaitSecondsRange.y);

        yield return new WaitForSeconds(m_WaitSeconds);

        // Generate a random enemy
        int enemyIndex = Random.Range(0, m_Enemies.Length);
        // Generate a random height
        float yPos = Random.Range(m_YMin.position.y, m_YMax.position.y);
        // Spawn
        Instantiate(m_Enemies[enemyIndex], new Vector3(transform.position.x, yPos, transform.position.z), Quaternion.identity);

        // Repeat the method
        StartCoroutine(Spawn());
    }

    #endregion
}
