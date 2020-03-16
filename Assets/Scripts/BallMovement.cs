using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _body;
    private Vector2 _moveDirectionNormalize;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _moveDirectionNormalize = Vector2.right;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void FixedUpdate()
    {
        _body.velocity = new Vector2(_moveDirectionNormalize.x * _movementSpeed, _body.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);

        ContactPoint2D rightPoint = contacts[0];
        foreach (var contact in contacts)
        {
            if (contact.point.x > rightPoint.point.x)
                rightPoint = contact;
        }

        if (rightPoint.normal.y < 0.65)
        {
            _moveDirectionNormalize = Vector2.zero;
            return;
        }

        Vector2 moveAlongGround = new Vector2(rightPoint.normal.y, -rightPoint.normal.x);
        _moveDirectionNormalize = moveAlongGround.normalized;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _moveDirectionNormalize = Vector2.right;
    }
}
