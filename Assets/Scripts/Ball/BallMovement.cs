using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _acceleration;

    private Rigidbody2D _body;
    private Vector2 _direction;
    private bool _onGround;

    public Vector2 Velocity => _body.velocity;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _direction = Vector2.right;
        _onGround = false;
    }

    private void Update()
    {
        if (_onGround && Input.GetKeyDown(KeyCode.Space))
            _body.AddForce(Vector2.up * _jumpForce);
    }

    private void FixedUpdate()
    {
        float velocityX = Mathf.Lerp(_body.velocity.x, _direction.x * _speed, _acceleration * Time.fixedDeltaTime);
        _body.velocity = new Vector2(velocityX, _body.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D rightPoint = GetRightPoint(collision);

        Vector2 directionAlongGround = new Vector2(rightPoint.normal.y, -rightPoint.normal.x);
        _direction = directionAlongGround.normalized;

        _onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _direction = Vector2.right;
        _onGround = false;
    }

    private ContactPoint2D GetRightPoint(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);

        ContactPoint2D rightPoint = contacts[0];
        foreach (var contact in contacts)
        {
            if (contact.point.x > rightPoint.point.x)
                rightPoint = contact;
        }

        return rightPoint;
    }
}