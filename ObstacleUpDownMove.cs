using System.Collections;
using UnityEngine;

public class ObstacleUpDownMove : MonoBehaviour
{
    [SerializeField] Transform ThornObj;
    [SerializeField] float speed;
    [SerializeField] float moveAmount;
    [SerializeField] float durationTime;

    Vector3 _startPos;
    Vector3 _TargetPos;

    void Start()
    {
        _startPos = ThornObj.position;
        _TargetPos = _startPos + Vector3.up * moveAmount;

    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 newPos = Vector3.Lerp(_startPos, _TargetPos, t);
        ThornObj.position = newPos;
    }
}
