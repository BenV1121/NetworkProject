using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{
    public float m_DampTime = .2f;
    public float m_ScreenEdgeBUffer = 4f;
    public float m_MinSize = 6.5f;
    public Transform[] m_Players;

    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        m_Players = (Transform[])FindObjectsOfType(typeof(Transform[]));
    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
    }


    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }

    private void FindAveragePosition()
    {
        Vector2 averagePos = new Vector2();
        int numPlayers = 0;

        for (int i = 0; i < m_Players.Length; i++)
        {
            if (!m_Players[i].gameObject.activeSelf)
                continue;

            Vector2 playerV2Pos = new Vector2(m_Players[i].position.x, m_Players[i].position.y);

            averagePos += playerV2Pos;
            numPlayers++;
        }
        if (numPlayers > 0)
            averagePos /= numPlayers;

        averagePos.y = transform.position.y;

        m_DesiredPosition = averagePos;
    }

    private void Zoom()
    {
        // We're finding the required size and smoothly damping towards that size.
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }

    private float FindRequiredSize()
    {
        Vector2 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        float size = 0f;

        for (int i = 0; i < m_Players.Length; i++)
        {
            if (!m_Players[i].gameObject.activeSelf)
                continue;

            Vector2 targetLocalPos = transform.InverseTransformPoint(m_Players[i].position);

            Vector2 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x / m_Camera.aspect));
        }
        size += m_ScreenEdgeBUffer;

        size = Mathf.Max(size, m_MinSize);

        return size;
    }

    public void setStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = m_DesiredPosition;

        m_Camera.orthographicSize = FindRequiredSize();
    }
}
