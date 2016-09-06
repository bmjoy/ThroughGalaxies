﻿using UnityEngine;
using System.Collections;

public enum BulletSource
{
    player,
    enemy,
}

public class Bullet : MonoBehaviour {

    public BulletSource bulletSource;
    public GameObject[] explosions;
    public float initialSpeed;
    public float accelerationDelay;
    public float acceleration;
    public float accAcceleration;
    public float damage;

    private float _startTime;

    void Start ()
    {
        // initial speed
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * initialSpeed;

        // record the start time for acceleration delay
        _startTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Time.time - _startTime >= accelerationDelay)
        {
            // acceleration
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            acceleration += accAcceleration;
            rigidbody.velocity += transform.forward * acceleration;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ignore boundary and other bullets 
        if (other.tag == "Boundary"
            || other.tag == "Bullet"
            || other.tag == "Powerup")
        {
            // do nothing
            return;
        }

        // ignore player (bullets from player)
        else if (other.tag == "Player"
                    && bulletSource == BulletSource.player)
        {
            // do nothing
            return;
        }

        // ignore enemies (bullets from enemies)
        else if (other.tag == "Enemy"
                    && bulletSource == BulletSource.enemy)
        {
            // do nothing
            return;
        }

        // hit anything else Damageable
        else
        {
            // apply damage to it
            Damageable target = other.GetComponent<Damageable>();
            if(target != null)
                target.applyDamage(damage);

            // do things on destroy
            destroy();
        }
    }

    protected virtual void destroy()
    {
        // explosion
        foreach(GameObject obj in explosions)
            Instantiate(obj, transform.position, transform.rotation);

        // destroy game object
        Destroy(gameObject);
    }
}