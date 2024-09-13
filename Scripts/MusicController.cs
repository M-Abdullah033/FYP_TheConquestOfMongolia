using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance = null;

    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set this object as the instance and make it persistent
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If an instance already exists and it's not this instance, destroy this object to prevent duplicates
            Destroy(gameObject);
        }
    }
}
