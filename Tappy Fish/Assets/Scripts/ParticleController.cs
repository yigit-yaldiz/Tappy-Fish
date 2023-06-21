using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] float _multiplier;

    private ParticleSystem.EmissionModule _emissionModule;

    Fish _fish;


    private void Awake()
    {
        _fish = GetComponent<Fish>();
        _emissionModule = _particleSystem.emission;
    }

    private void FixedUpdate()
    {
        _emissionModule.rateOverTime = _fish.Velocity.y * _multiplier;   
    }
}
