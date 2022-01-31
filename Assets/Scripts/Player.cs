using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Variables

    // Player attributes shit
    [SerializeField] private float m_FuckingMovementSpeed;
    [SerializeField] private float m_FuckingJumpForce;
    [SerializeField] private int m_FuckingBaseJumpCount;
    [SerializeField] private float m_FuckingDashForce;
    [SerializeField] private float m_FuckingDashDuration;

    // Player runtime attributes shit
    private int m_FuckingCurrentJumpCount;

    // Input shit
    private float m_FuckingInput;
    private bool m_FuckingJump;
    private bool m_FuckingDash;

    // Reference shit
    private Rigidbody2D m_FuckingRigidbody2D;
    private BoxCollider2D m_FuckingBoxCollider2D;
    [SerializeField] private LayerMask m_FuckingGround;
    [SerializeField] private Color32 m_FuckingNormalColor;
    [SerializeField] private Color32 m_FuckingDashColor;
    [SerializeField] private SpriteRenderer m_FuckingSpriteRenderer;

    private bool m_FuckingDoGroundCheck;
    private bool m_FuckingCanDash;
    private bool m_FuckingFacingRight;

    #endregion

    #region Start

    private void Start()
    {
        m_FuckingRigidbody2D = GetComponent<Rigidbody2D>();
        m_FuckingBoxCollider2D = GetComponent<BoxCollider2D>();
        m_FuckingSpriteRenderer.color = m_FuckingNormalColor;
        m_FuckingCanDash = true;
    }

    #endregion

    #region Update

    private void Update()
    {
        m_FuckingInput = Input.GetAxisRaw("Horizontal");
        m_FuckingJump = Input.GetButtonDown("Jump");
        m_FuckingDash = Input.GetKeyDown(KeyCode.LeftShift);

        // Check which side the character is facing
        if (m_FuckingInput > 0)
        {
            m_FuckingFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (m_FuckingInput < 0)
        {
            m_FuckingFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Move();
        GroundCheck();
    }

    #endregion

    #region FixedUpdate

    private void FixedUpdate()
    {
        // Enable ground check
        if (m_FuckingRigidbody2D.velocity.y < 0)
        {
            m_FuckingDoGroundCheck = true;
        }
    }

    #endregion

    #region Move

    private void Move()
    {
        if (m_FuckingCanDash)
        {
            m_FuckingRigidbody2D.velocity = new Vector2(m_FuckingInput * m_FuckingMovementSpeed, m_FuckingRigidbody2D.velocity.y);
        }

        Jump();

        // Dash
        if (m_FuckingDash && m_FuckingCanDash)
        {
            m_FuckingSpriteRenderer.color = m_FuckingDashColor;
            m_FuckingCanDash = false;
            if (m_FuckingFacingRight)
            {
                m_FuckingRigidbody2D.velocity = new Vector2(m_FuckingDashForce, m_FuckingRigidbody2D.velocity.y);
            }
            else
            {
                m_FuckingRigidbody2D.velocity = new Vector2(-m_FuckingDashForce, m_FuckingRigidbody2D.velocity.y);
            }
            StartCoroutine(Dash());
        }
    }

    #endregion

    #region Dash

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(m_FuckingDashDuration);
        m_FuckingSpriteRenderer.color = m_FuckingNormalColor;
        m_FuckingRigidbody2D.velocity = new Vector2(0, m_FuckingRigidbody2D.velocity.y);
        m_FuckingCanDash = true;
    }

    #endregion=

    #region Jump

    private void Jump()
    {
        // Jump
        if (m_FuckingCurrentJumpCount > 0 && m_FuckingJump)
        {
            m_FuckingCurrentJumpCount--;
            m_FuckingRigidbody2D.velocity = new Vector2(m_FuckingRigidbody2D.velocity.x, 0);
            m_FuckingRigidbody2D.AddForce(new Vector2(0f, m_FuckingJumpForce * 100));
        }
    }

    #endregion

    #region Ground Check
    private void GroundCheck()
    {
        float extraExtents = 0.1f; // Default was 0.05f
        RaycastHit2D rayCastHit = Physics2D.BoxCast(m_FuckingBoxCollider2D.bounds.center, m_FuckingBoxCollider2D.bounds.size, 0f, Vector2.down, extraExtents, m_FuckingGround);

        // Ground check
        if (m_FuckingDoGroundCheck)
        {
            if (rayCastHit.collider != null)
            {
                m_FuckingDoGroundCheck = false;
                m_FuckingCurrentJumpCount = m_FuckingBaseJumpCount;
            }
        }
    }
    #endregion

    #region Collisions

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
        if (collider.CompareTag("Enemy") && m_FuckingCanDash)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
