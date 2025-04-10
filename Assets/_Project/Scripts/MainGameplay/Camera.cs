using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _offset;
    private float _speed = 10f;

    private void Start()
    {
        _offset = new Vector3(0f, 2f, -4f);
    }

    private void FixedUpdate()
    {
        var targetPosition = _player.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);

        var direction = _player.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);
    }
}
