using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private const string VolumePrefKey = "GameVolume";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.5f);
        AudioListener.volume = savedVolume;


        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }
}
