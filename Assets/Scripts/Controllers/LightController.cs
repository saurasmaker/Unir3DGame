using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightController : MonoBehaviour
{
    [SerializeField]
    bool _turnedOn = true;

    public UnityEvent OnTurnOn, OnTurnOff;

    private void Start()
    {
        if(_turnedOn)
            TurnOn();
        else
            TurnOff();
    }

    public void TurnOn()
    {
        OnTurnOn?.Invoke();
    }

    public void TurnOff()
    {
        OnTurnOff?.Invoke();
    }
}
