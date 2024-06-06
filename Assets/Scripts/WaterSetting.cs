using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterSetting : Singleton<WaterSetting>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float timeOn = 10f;

    public void WaterOn()
    {
        rb.simulated = true;
        Invoke(nameof(WaterOff), timeOn);
    }

    private void WaterOff()
    {
        rb.simulated = false;
    }
}
