using UnityEngine;
using System.Collections;

public class BulletBehavior : ADieableBehavior
{
    public float speed;
    public float lifetime_remaining = 1;
    public GameObject spawn;

    void Start ()
    {
        this.GetComponent<Rigidbody>().velocity += transform.forward * speed;
    }

    void Update()
    {
        if (lifetime_remaining <= 0)
        {
            Die();
        }
        lifetime_remaining -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDying)
            return;
        if (this.tag != "GravBullet")
        {
            if (other.tag == "Asteroid" ||
                (tag == "PlayerBullet" && other.tag == "AlienShip") ||
                (tag == "AlienBullet" && other.tag == "PlayerShip"))

            {
                var other_des = other.GetComponent<ADieableBehavior>();
                if (other_des)
                {

                    other_des.Die();

                    this.Die();
                }
            }
        }
        else if (other.tag == "Asteroid")
        {
            var other_rig = other.GetComponent<Rigidbody>();
            if (other_rig)
            {

                other_rig.AddForce(-this.GetComponent<Rigidbody>().velocity * 10, ForceMode.Impulse);

                this.Die();
            }
        }
    }

    public override void Die()
    {
        if (isDying)
            return;
        isDying = true;

        if (spawn)
        { 
        var spawnling = (GameObject)Instantiate(spawn, this.transform.position, Quaternion.identity);
        var spawnling_wrap = spawnling.GetComponent<WarpingBehavior>();
        if (spawnling_wrap)
            spawnling_wrap.boundary = this.GetComponent<WarpingBehavior>().boundary;
        }
        base.Die();
    }
}
