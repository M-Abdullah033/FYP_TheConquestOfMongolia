using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Transform rightHandTransform; // Public reference to player's right hand bone
    public GameObject[] weaponPrefabs; // Array of weapon prefabs
    private GameObject[] weapons; // Array to store instantiated weapons
    private int currentWeaponIndex = 0; // Index of the currently equipped weapon

    void Start()
    {
        weapons = new GameObject[weaponPrefabs.Length];
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            GameObject weaponInstance = Instantiate(weaponPrefabs[i], rightHandTransform); // Ensure correct parent object
            weaponInstance.SetActive(false); // Deactivate initially
            weapons[i] = weaponInstance;
        }

        SetActiveWeapon(currentWeaponIndex); // Activate the first weapon
    }

    void Update()
    {
        // Check for input to switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
        // ... (add key bindings for other weapons)
    }

    void SwitchWeapon(int newIndex)
    {
        if (newIndex >= 0 && newIndex < weapons.Length)
        {
            SetActiveWeapon(currentWeaponIndex, false); // Deactivate the current weapon

            SetActiveWeapon(newIndex, true); // Activate the new weapon

            currentWeaponIndex = newIndex;
        }
    }

    void SetActiveWeapon(int weaponIndex, bool isActive = true)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            weapons[weaponIndex].SetActive(isActive);
        }
    }
}
