using System.Security;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    public float damage = 10f;
    public float range = 100f;

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
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
