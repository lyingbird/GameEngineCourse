using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask))
        {
            OnHitObject(hit);
        }
    }


    void OnHitObject(RaycastHit hit)
    {
        
        Debug.Log("hitted:" + hit.collider.gameObject.name); 
        enemyController enemy = hit.collider.GetComponent<enemyController>();
        if (enemy != null)
        {
            DemoVehicleRoot.killedNumber++;
            enemy.Die(this.transform);
        }
        GameObject.Destroy(gameObject);
    }

   

}
