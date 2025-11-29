using System;
using UnityEngine;

public class HammerRotation : MonoBehaviour
{


[SerializeField] private float _rotationSpeed;
[SerializeField] private float _angel;


    void Awake()
    {
     
    }
    void Update()
    {
        float t = Mathf.PingPong(Time.time * _rotationSpeed,1f);
        float currentAngel = Mathf.Lerp(-_angel , _angel,t);
        transform.localRotation=Quaternion.Euler(0,0,currentAngel);
        
    }
}
