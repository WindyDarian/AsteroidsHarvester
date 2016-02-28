using UnityEngine;
using System.Collections;

public class UsedBulletBehavior : ADieableBehavior
{
    public float rotationRate = 270;

    // Use this for initialization
    void Start () {
        var direction = AsteroidsMathHelper.randomDirectionXZ();
        this.GetComponent<Rigidbody>().velocity = direction * Random.Range(1, 2);

    }

    // Update is called once per frame
    void Update () {
        var rb = this.GetComponent<Rigidbody>();
        rb.rotation = rb.rotation * Quaternion.AngleAxis(Time.deltaTime * this.rotationRate, Vector3.up);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDying)
            return;

        if (other.tag == "PlayerShip")
        {
            var playerb = other.GetComponent<PlayerShipBehavior>();
            if (playerb)
            {

                 playerb.remainingBullet += 1;
                 this.Die();

            }
        }
    }

    public override void Die()
    {
        if (isDying)
            return;
        isDying = true;
        AsteroidsAudio.playClipAt(this.GetComponents<AudioSource>()[0].clip, transform.position);


        base.Die();
    }


}
