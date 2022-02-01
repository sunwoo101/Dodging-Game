using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Variables

    // Player attributes
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private int m_BaseJumpCount;
    private int m_CurrentJumpCount;
    [SerializeField] private float m_DashForce;
    [SerializeField] private float m_DashDuration;

    // Input
    private float m_Input;
    private bool m_Jump;
    private bool m_Dash;

    // Reference
    private Rigidbody2D m_Rigidbody2D;
    private BoxCollider2D m_BoxCollider2D;
    [SerializeField] private LayerMask m_Ground;
    [SerializeField] private Color32 m_NormalColor;
    [SerializeField] private Color32 m_DashColor;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private GameManager m_GameManager;

    // Checks
    private bool m_DoGroundCheck;
    private bool m_CanDash;
    private bool m_FacingRight;

    #endregion

    #region Start

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        m_SpriteRenderer.color = m_NormalColor;
        m_CanDash = true;
    }

    #endregion

    #region Update

    private void Update()
    {
        m_Input = Input.GetAxisRaw("Horizontal");
        m_Jump = Input.GetButtonDown("Jump");
        m_Dash = Input.GetKeyDown(KeyCode.LeftShift);

        // Check which side the character is facing and makes the character face that direction (only works if you have a character sprite thats looking at the side)
        if (m_Input > 0)
        {
            m_FacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (m_Input < 0)
        {
            m_FacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Move();
        GroundCheck();
    }

    #endregion

    #region FixedUpdate

    private void FixedUpdate()
    {
        // Enable ground check (we do ground check like this so double jump will work properly without giving extra or less jumps)
        if (m_Rigidbody2D.velocity.y < 0)
        {
            m_DoGroundCheck = true;
        }
    }

    #endregion

    #region Move

    private void Move()
    {
        if (m_CanDash)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Input * m_MovementSpeed, m_Rigidbody2D.velocity.y);
        }

        Jump();

        // Dash
        if (m_Dash && m_CanDash)
        {
            m_SpriteRenderer.color = m_DashColor;
            m_CanDash = false;
            // Dash towards the direction the player is facing
            if (m_FacingRight)
            {
                m_Rigidbody2D.velocity = new Vector2(m_DashForce, m_Rigidbody2D.velocity.y);
            }
            else
            {
                m_Rigidbody2D.velocity = new Vector2(-m_DashForce, m_Rigidbody2D.velocity.y);
            }
            StartCoroutine(Dash());
        }
    }

    #endregion

    #region Dash

    // This is not the actual dash but is a delay and resets character speed and colour to the normal colour after the dash
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(m_DashDuration);
        m_SpriteRenderer.color = m_NormalColor;
        m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
        m_CanDash = true;
    }

    #endregion

    #region Jump

    private void Jump()
    {
        // Jump
        if (m_CurrentJumpCount > 0 && m_Jump)
        {
            m_CurrentJumpCount--;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * 100));
        }
    }

    #endregion

    #region Ground Check
    private void GroundCheck()
    {
        float extraExtents = 0.1f; // Default was 0.05f
        RaycastHit2D rayCastHit = Physics2D.BoxCast(m_BoxCollider2D.bounds.center, m_BoxCollider2D.bounds.size, 0f, Vector2.down, extraExtents, m_Ground);

        // Ground check
        if (m_DoGroundCheck)
        {
            if (rayCastHit.collider != null)
            {
                m_DoGroundCheck = false;
                m_CurrentJumpCount = m_BaseJumpCount;
            }
        }
    }
    #endregion

    #region Collisions

    // Doing collisions like this makes sure collision works with obstacles with or without rigidbodies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject collider)
    {
        // Collide with enemy only if not dashing
        if (collider.CompareTag("Enemy") && m_CanDash)
        {
            m_GameManager.GameOver();
            Destroy(gameObject);
        }

        if (collider.CompareTag("Coin"))
        {
            m_GameManager.Score += 1;
            Destroy(collider.gameObject);
        }
    }

    #endregion
}
