using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject bulletPrefab;
    public GameObject shootFrom;
    public GameManager gameManager;
    public int health;
    public float nextBullet;
    Rigidbody body;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        nextBullet = 0;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject != null)
        {
            if(Vector3.Distance(transform.position, targetObject.transform.position) > 3f)
            {
                transform.LookAt(targetObject.transform);
                body.velocity = transform.forward * 2f;
            }

            if (nextBullet < Time.timeSinceLevelLoad)
            {
                GameObject newBullet = Instantiate(bulletPrefab);

                newBullet.transform.position = shootFrom.transform.position;
                newBullet.transform.rotation = shootFrom.transform.rotation;

                Rigidbody bulletBody = newBullet.GetComponent<Rigidbody>();
                bulletBody.velocity = bulletBody.transform.forward * 20f;
                Destroy(newBullet, 3f);
                nextBullet = Time.timeSinceLevelLoad + 1f;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            gameManager.hitBullets++;
            if (health > 0)
                health--;

            else
            {
                gameManager.kills++;
                Destroy(gameObject);
            }
                
        }
    }
}
