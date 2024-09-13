using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound; // The click sound effect
    private AudioSource clickAudioSource;

    void Start()
    {
        clickAudioSource = GetComponent<AudioSource>();
        if (clickAudioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the ButtonClickSound GameObject.");
        }
    }

    // This method will be called when the button is clicked
    public void PlayClickSound()
    {
        if (clickAudioSource != null && clickSound != null)
        {
            clickAudioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("Click sound or AudioSource is not assigned.");
        }
    }
}
