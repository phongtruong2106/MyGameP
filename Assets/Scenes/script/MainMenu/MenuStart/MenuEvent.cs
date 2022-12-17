using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MenuEvent : MonoBehaviour
{
    [SerializeField] private Slider volume;
    [SerializeField] private AudioMixer audioMixer;

    private float value;

    private void Start()
    {
       audioMixer.GetFloat("Volume", out value);
        volume.value = value;
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", volume.value);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
