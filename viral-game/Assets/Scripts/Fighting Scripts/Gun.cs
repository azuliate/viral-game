using System.Security;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Objects")]
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Stats")]
    public float damage = 10f;
    public float range = 100f;

    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shootKey))
        {
            Shoot();
        }    
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        //Raycast to enemy
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
