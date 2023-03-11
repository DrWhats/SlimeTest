using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchTransform;
    public float launchSpeed = 20f;
    public float launchAngle = 45f;
    public int damage;

    private GameObject target;


    public void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchTransform.position, Quaternion.identity);

        Vector3 targetPos = target.transform.position;
        float distanceToTarget = Vector3.Distance(launchTransform.position, targetPos);

        float projectileTime = distanceToTarget / launchSpeed;
        float projectileVelocity = launchSpeed * Mathf.Cos(launchAngle * Mathf.Deg2Rad);
        float projectileHeight = launchTransform.position.y + (projectileVelocity * projectileTime * Mathf.Sin(launchAngle * Mathf.Deg2Rad)) - (0.5f * Physics.gravity.magnitude * Mathf.Pow(projectileTime, 2));

        Vector3 launchVelocity = CalculateLaunchVelocity(targetPos, projectileTime, projectileHeight);

        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = launchVelocity;
    }

    Vector3 CalculateLaunchVelocity(Vector3 targetPosition, float projectileTime, float projectileHeight)
    {
        Vector3 displacement = targetPosition - launchTransform.position;
        Vector3 horizontal = displacement;
        horizontal.y = 0;

        float horizontalDistance = horizontal.magnitude;
        float verticalDistance = displacement.y;

        float time = Mathf.Sqrt(-2 * projectileHeight / Physics.gravity.y) + Mathf.Sqrt(2 * (verticalDistance - projectileHeight) / Physics.gravity.y);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * projectileHeight);
        Vector3 velocityXZ = horizontal / time;

        return velocityXZ + velocityY * -Mathf.Sign(Physics.gravity.y);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    
}
