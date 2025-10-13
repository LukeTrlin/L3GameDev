using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerFunctions : MonoBehaviour
{
    public GameObject OptionsPanelPrefab; // Reference to the OptionsPanel prefab
    private GameObject optionsPanelInstance; // Instance of the OptionsPanel
    private GlobalVariables globalVariables;

    public GameObject bulletPrefab; // Reference to the bullet prefab
    private GameObject bullet;

    private int currentAmmo;

    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        currentAmmo = globalVariables.currentAmmo;
        // Instantiate the OptionsPanel prefab and deactivate it initially
        if (OptionsPanelPrefab != null)
        {
            optionsPanelInstance = Instantiate(OptionsPanelPrefab);
       
        }
    }

    private float fireCooldown = 0.1f;
    private float lastFireTime = -Mathf.Infinity;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastFireTime >= fireCooldown && currentAmmo > 0)
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
                // If nothing is hit, shoot in the direction of the ray
                targetDirection = (ray.origin + ray.direction * 100f - transform.position).normalized;
            }

            // Allow shooting in all directions (including up/down)
            targetDirection = targetDirection.normalized;

            // Set bullet rotation to face the direction (including vertical)
            Quaternion bulletRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(targetDirection * 100f, ForceMode.Impulse);
            Destroy(bullet, 2f); // Destroy the bullet after 2 seconds  
        }

        if (Input.GetKeyDown(globalVariables.reload) && globalVariables.playerMaxAmmo >= 30)
        {
            currentAmmo = 30;
            globalVariables.currentAmmo = 30;
        }
        else if (Input.GetKeyDown(globalVariables.reload) && globalVariables.playerMaxAmmo < 30)
        {
            currentAmmo = globalVariables.playerMaxAmmo;
            globalVariables.currentAmmo = currentAmmo;
        }

        if (Input.GetKeyDown(globalVariables.pause) && optionsPanelInstance != null)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            optionsPanelInstance.SetActive(true); // Toggle the options panel visibility
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Block")
        {
            Rigidbody block = collision.gameObject.GetComponent<Rigidbody>();
            if (block != null && collision.contacts.Length > 0)
            {
                // Calculate push direction based on collision normal
                Vector3 pushDirection = new Vector3(-collision.contacts[0].normal.x, 0, 0);
                block.velocity = pushDirection * 5f; // Adjust speed as needed
            }
       
      
        }

    }

}
