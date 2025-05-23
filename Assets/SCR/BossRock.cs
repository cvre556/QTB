using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : Bullet
{

    Rigidbody rigid;
    float angularPowar = 2;
    float scaleValue = 0.1f;
    bool isShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }
        IEnumerator GainPower()
        {
            while (!isShoot)
            {
                angularPowar += 0.02f;
                scaleValue += 0.005f;
                transform.localScale = Vector3.one * scaleValue;
                rigid.AddTorque(transform.right * angularPowar, ForceMode.Acceleration);
                yield return null;
            }
        }
}
