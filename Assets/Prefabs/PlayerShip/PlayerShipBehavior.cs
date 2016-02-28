using UnityEngine;
using System.Collections;

public class PlayerShipBehavior : ADieableBehavior {

    public float engineForce = 2000;
    public float turnRate = 360;
    public float firing_cooldown = 0.25f;
    public float firing_cooldown_remaining = 0.0f;


    public GameObject bulletPreFab;
    public GameObject gravBulletPreFab;

    public Transform weaponPos;

    public int maxBullet;
    public int remainingBullet;

    private float invincibleTimerRemain;
    

    void Start()
    {
        invincibleTimerRemain = 2.0f;
        remainingBullet = maxBullet;
    }

    void Update()
    {
        if (invincibleTimerRemain > 0)
            invincibleTimerRemain -= Time.deltaTime;

        if (firing_cooldown_remaining > 0)
            firing_cooldown_remaining -= Time.deltaTime;

        //if (Input.GetButtonDown("Fire1") && firing_cooldown_remaining <= 0)
        if (Input.GetButtonDown("Fire1"))
        {
            if ( this.remainingBullet > 0)
            { 
            //fi && this.remainingBullet > 0ring_cooldown_remaining += firing_cooldown;
            var bullet = (GameObject)Instantiate(bulletPreFab, weaponPos.position, weaponPos.rotation);
            bullet.GetComponent<Rigidbody>().velocity += this.GetComponent<Rigidbody>().velocity;
            var bullet_wrap = bullet.GetComponent<WarpingBehavior>();
            if (bullet_wrap)
                bullet_wrap.boundary = this.GetComponent<WarpingBehavior>().boundary;
            this.remainingBullet -= 1;
            //GetComponent<AudioSource>().Play();
            AsteroidsAudio.playClipAt(this.GetComponents<AudioSource>()[1].clip, transform.position);
            }
            else
            {
                AsteroidsAudio.playClipAt(this.GetComponents<AudioSource>()[2].clip, transform.position);
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (this.firing_cooldown_remaining <= 0)
            { 
                firing_cooldown_remaining += firing_cooldown;
                var bullet = (GameObject)Instantiate(gravBulletPreFab, weaponPos.position, weaponPos.rotation);
                bullet.GetComponent<Rigidbody>().velocity += this.GetComponent<Rigidbody>().velocity;
                var bullet_wrap = bullet.GetComponent<WarpingBehavior>();
                if (bullet_wrap)
                    bullet_wrap.boundary = this.GetComponent<WarpingBehavior>().boundary;
                //GetComponent<AudioSource>().Play();
                AsteroidsAudio.playClipAt(this.GetComponents<AudioSource>()[3].clip, transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        float input_engine = Mathf.Clamp01(Input.GetAxis("Move"));
        float input_rotation = Input.GetAxis("Horizontal");

        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(transform.forward * input_engine * this.engineForce);
        rigidbody.rotation = rigidbody.rotation * Quaternion.Euler(0.0f, input_rotation * turnRate * Time.deltaTime, 0.0f);

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    public override void Die()
    {
        if (isDying)
            return;

        if (invincibleTimerRemain > 0)
            return;


        AsteroidsAudio.playClipAt(this.GetComponents<AudioSource>()[0].clip, transform.position);

        base.Die();


        //Sound and particle
    }
}
