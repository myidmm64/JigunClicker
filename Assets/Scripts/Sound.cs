using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    private AudioClip touch = null;
    private AudioSource audioSource = null;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = touch;
    }
    public void AtK()
    {
        audioSource.Play();
    }
}
