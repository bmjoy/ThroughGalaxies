﻿using UnityEngine;
using System.Collections;

public class Razer : Enemy {

    public Limit durationStart;
    public Limit durationHorizontal;
    public Limit durationStraight;

    private Rigidbody _rigidbody;

	new void Start ()
    {
        // from Enemy (also Damageable)
        base.Start();

        // move down
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * moveSpeed;

        // wander horizontally randomly
        StartCoroutine(wander());
    }

    private IEnumerator wander()
    {
        // start wait
        yield return new WaitForSeconds(Random.Range(durationStart.min, durationStart.max));

        while(true)
        {
            // move horizontally for a while
            float sign = -Mathf.Sign(transform.position.x);
            _rigidbody.velocity = new Vector3(moveSpeed * sign, _rigidbody.velocity.y, _rigidbody.velocity.z);
            yield return new WaitForSeconds(Random.Range(durationHorizontal.min, durationHorizontal.max));

            // move straight for a while
            _rigidbody.velocity = new Vector3(0.0f, _rigidbody.velocity.y, _rigidbody.velocity.z);
            yield return new WaitForSeconds(Random.Range(durationStraight.min, durationStraight.max));
        }
    }
}