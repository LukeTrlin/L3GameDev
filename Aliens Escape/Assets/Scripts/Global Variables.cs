using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{

    [Header("Global Variables")]

    [Header("Game Settings")]
    public int difficultyLevel = 0; // 0: Easy, 1: Medium, 2: Hard
    public bool isSoundEnabled = true; // Sound effects enabled by default
    public bool isMusicEnabled = true; // Music enabled by default

    public int soundLevel = 100;
    public int musicLevel = 100;


    [Header("Player Stats")]
    public int playerScore = 0;
    public int playerHealth = 100; // Default health value
    public int playerGunDamage = 20; // Default gun damage value

    public int playerAmmo = 30; // Default ammo count
    public int playerMaxAmmo = 90; // Default max ammo count
    public int playerMeleeDamage = 10; // Default melee damage value

    [Header("Keybinds Settings")]

    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;

    public int shoot = 0; // Left mouse button
    public KeyCode reload = KeyCode.R; // Reload key
    public KeyCode Interact = KeyCode.E; // Interact key

    public KeyCode pause = KeyCode.Escape; // Pause key


    public float TimeTaken = 0.0f;

    public int enemyKills = 0;




    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(gameObject); // Prevents this object from being destroyed when loading a new scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
