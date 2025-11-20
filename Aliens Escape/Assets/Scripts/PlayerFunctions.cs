using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFunctions : MonoBehaviour
{
    private GlobalVariables globalVariables;
    public MusicController musicController; // Cached MusicController instance

    public GameObject bulletPrefab; // Reference to the bullet prefab
    private GameObject bullet;

    private int currentAmmo;

    private float fireCooldown = 0.1f;
    private float lastFireTime = -Mathf.Infinity;

    public GameObject optionsMenuInstance; // Reference to the persistent OptionsMenu

    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        currentAmmo = globalVariables.currentAmmo;

        // Cache MusicController instance
    

        // Reference the persistent OptionsMenu
       
    }

    void Update()
    {
        // Shooting
        if (Input.GetMouseButtonDown(0) && Time.time - lastFireTime >= fireCooldown && currentAmmo > 0 && globalVariables.isOptionsOpen == false)
        {
            lastFireTime = Time.time;
            currentAmmo -= 1;
            globalVariables.playerMaxAmmo -= 1;
            globalVariables.currentAmmo -= 1;

            // Ray from camera to mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetDirection;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetDirection = (hit.point - transform.position).normalized;
            }
            else
            {
                targetDirection = (ray.origin + ray.direction * 100f - transform.position).normalized;
            }

            Quaternion bulletRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(targetDirection * 100f, ForceMode.Impulse);
            Destroy(bullet, 2f);

            // Play shooting sound
            if (musicController != null)
                musicController.PlayShootSound();
        }

        // Reloading
        if (Input.GetKeyDown(globalVariables.reload))
        {
            if (globalVariables.playerMaxAmmo >= 30)
                currentAmmo = 30;
            else
                currentAmmo = globalVariables.playerMaxAmmo;

            globalVariables.currentAmmo = currentAmmo;

            // Play reload sound
            if (musicController != null)
                musicController.PlayReloadSound();
        }

        // Pause / OptionsMenu
        if (Input.GetKeyDown(globalVariables.pause) && optionsMenuInstance != null)
        {
            globalVariables.isOptionsOpen = true;
            Cursor.lockState = CursorLockMode.None; // Set lock state to None
            Cursor.visible = true; // Make the cursor visible
            Time.timeScale = 0;
            optionsMenuInstance.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Block") && collision.contacts.Length > 0)
        {
            Rigidbody block = collision.gameObject.GetComponent<Rigidbody>();
            if (block != null)
            {
                Vector3 pushDirection = new Vector3(-collision.contacts[0].normal.x, 0, 0);
                block.velocity = pushDirection * 5f;
            }
        }
    }
}
