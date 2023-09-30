using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _speed;

    private Vector3 m_input;


    // Update is called once per frame
    void Update()
    {
        GatherInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        m_input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        var isoRotation = Quaternion.Euler(0, 45, 0);
        var delta = isoRotation * m_input;
        _rigidbody.MovePosition(transform.position + delta * _speed * Time.deltaTime);
    }
}
