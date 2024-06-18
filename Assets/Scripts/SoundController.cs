using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource[] audioSources;

    private const string VolumePrefKey = "Volume";

    void Start()
    {
        
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, audioSources.Length > 0 ? audioSources[0].volume : 1.0f);

      
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;

        
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        
        SetVolume(value);

        
        PlayerPrefs.SetFloat(VolumePrefKey, value);
    }

    void SetVolume(float volume)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
}
