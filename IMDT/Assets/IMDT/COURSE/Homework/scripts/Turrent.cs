using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    public Transform rotateTransform;
    public Camera viewCamera;

    float nextShotTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {

        // Look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            this.LookAt(point, rotateTransform);
        }

        if (Input.GetMouseButton(0))
        {
           // Debug.Log("shooting!");
            Shoot();
        }
    }

    public void LookAt(Vector3 lookPoint,Transform rotateRransform)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y - 0.2f, lookPoint.z);
        rotateRransform.LookAt(heightCorrectedPoint);
    }

    public void Shoot()
    {

        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
