using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    private const string VolumePrefKey = "Volume";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, audioSource.volume);
        audioSource.volume = savedVolume;
        volumeSlider.value = savedVolume;

        
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        
        audioSource.volume = value;
        PlayerPrefs.SetFloat(VolumePrefKey, value);
    }

  
}
