using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class UpDownPlatformMove : MonoBehaviour
{

    [SerializeField] private float moveAmount=10f;
    [SerializeField] private float moveSpeed=0.3f;

    private Rigidbody _playerRb;
    private Rigidbody _PlatformRb;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private Vector3 _lastRbPos;

    void Awake()
    {
        _PlatformRb = GetComponent<Rigidbody>();
        _PlatformRb.isKinematic = true;
    }

    void Start()
    {
        _startPos = transform.position;
        _targetPos = transform.position + Vector3.up * moveAmount;
        _lastRbPos = _PlatformRb.position;
    }

    void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed,1f);
        Vector3 newPos = Vector3.Lerp(_startPos,_targetPos,t);
        _PlatformRb.MovePosition(newPos);

        if (_playerRb != null)
        {
            Vector3 platformVelocity = (_PlatformRb.position - _lastRbPos) / Time.fixedDeltaTime;
            platformVelocity.y = 0;
            _playerRb.linearVelocity += platformVelocity;
        }
        _lastRbPos = _PlatformRb.position;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerRb = other.GetComponent<Rigidbody>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerRb=null;
        }
    }



    /* 
        [SerializeField] float moveAmount = 2f;
        [SerializeField] float moveSpeed = 2f;

        private Rigidbody _playerRb; // player Rigidbody referansı
        private Rigidbody _platformRb;
        private Vector3 _startPos;
        private Vector3 _targetPos;
        private Vector3 _lastPos;




        void Awake()
        {
            _platformRb = GetComponent<Rigidbody>();
            _platformRb.isKinematic = true;
        }
        void Start()
        {
            _startPos = transform.position;
            _targetPos = _startPos + Vector3.up * moveAmount;
            _lastPos = _platformRb.position;
        }

        void FixedUpdate()
        {
            float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
            Vector3 newPos = Vector3.Lerp(_startPos, _targetPos, t);
            _platformRb.MovePosition(newPos);

            if (_playerRb != null)
            {
                Vector3 platformVelocity = (_platformRb.position - _lastPos) / Time.fixedDeltaTime;
                platformVelocity.y = 0f;
                _playerRb.linearVelocity += platformVelocity;
            }

            _lastPos = _platformRb.position;

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _playerRb = other.GetComponent<Rigidbody>();
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _playerRb = null;
        } */
}









