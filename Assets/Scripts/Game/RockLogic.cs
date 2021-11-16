using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class RockLogic : MonoBehaviour
{
    private bool alreadyHit = false;
    private float lifeTime = 2f;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector3 dir)
    {
        GetComponent<Rigidbody>().velocity = dir.normalized * 20;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" && !alreadyHit)
        {
            alreadyHit = true;
            Destroy(gameObject);
            other.collider.GetComponentInParent<PlayerController>().Hitpoints -= 10;
        }
    }
}
