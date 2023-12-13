using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatManager : MonoBehaviour
{
    private bool _IsEat;

    private void Start()
    {
        _IsEat = false;
    }

    public bool GetIsEat()
    {
        return _IsEat;
    }

    public void SetIsEat()
    {
        _IsEat = true;
    }

    public void ResetIsEat()
    {
        _IsEat = false;
    }
}
