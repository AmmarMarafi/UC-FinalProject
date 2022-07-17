using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Rigidbody body;
    float speed = 5f;
    GameObject cameraObject;

    public GameObject BulletPrefab;
    public float nextBullet = 0f;
    public GameObject ShootFrom;
    public GameObject armObject;
    public GameObject lookFrom;
    public ParticleSystem smoke;
    public bool canJump = true;

    public float health;
    public Image imgHealth;
    public GameManager gameManager;
    public BotScript enemy;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if (health > 0)
                health--;

            else
                gameManager.EndGame();
        }

        if (collision.gameObject.name == "Ground")
            canJump = true;

        if (collision.gameObject.tag == "Bot")
            enemy = collision.gameObject.GetComponent<BotScript>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
            enemy = null;
    }
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        cameraObject = GameObject.FindObjectOfType<Camera>().gameObject;
        health = 100;
    }

   
    void Update()
    {
        imgHealth.fillAmount = health / 100f;

        if(body != null)
        {

            body.AddRelativeForce((Vector3.right * Input.GetAxis("Horizontal")
                + Vector3.forward * Input.GetAxis("Vertical")) * speed * 10f);
           
            /*body.velocity = (transform.right * Input.GetAxis("Horizontal") 
                + transform.forward * Input.GetAxis("Vertical")) * speed;*/

            /*Vector3 vel = body.velocity;
            vel.x = Input.GetAxis("Horizontal") * speed;
            vel.z = Input.GetAxis("Vertical") * speed;
            body.velocity = vel;*/
        }

        if(cameraObject != null)
        {
            cameraObject.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y"));
            Quaternion newRotation = cameraObject.transform.localRotation;

            if (newRotation.x > 0.6f)
                newRotation.x = 0.6f;

            else

                if (newRotation.x < -0.6f)
                newRotation.x = -0.6f;

            newRotation.z = 0;
            newRotation.y = 0;

            cameraObject.transform.localRotation = newRotation;
        }

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));

        RaycastHit rHit;
        if (Physics.Raycast(lookFrom.transform.position, lookFrom.transform.forward, out rHit))
        {
            armObject.transform.LookAt(rHit.point);
        }

        if (Input.GetMouseButton(0))
        {

            if(nextBullet < Time.timeSinceLevelLoad)
            {
                gameManager.totalBullets++;
                GameObject newBullet = Instantiate(BulletPrefab);
                newBullet.transform.position = ShootFrom.transform.position;
                newBullet.transform.rotation = ShootFrom.transform.rotation;

                Rigidbody bulletBody = newBullet.GetComponent<Rigidbody>();
                bulletBody.velocity = bulletBody.transform.forward * 20f;
                Destroy(newBullet, 3f);
                nextBullet = Time.timeSinceLevelLoad + 0.25f;
                smoke.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gameManager.gamePaused)
        {

            if (canJump)
            {
                Vector3 newVelocity = body.velocity;
                newVelocity.y += 3f;
                body.velocity = newVelocity;
                canJump = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (enemy != null)
            {
                enemy.health = 0;
            }
        }
    }
}
