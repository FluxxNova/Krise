using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    static string music_PPrefsTag = "Music";
    static string sfx_PPrefsTag = "SFX";
    static string resolution_PPrefsTag = "Resolution";
    static string fullscreen_PPrefsTag = "FullScreen";

    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightness = 1;

    public Slider slider;
    public Light sceneLight;

    private float _brightnessLevel;

    [SerializeField] AudioMixer myMixer;

    [SerializeField] TMP_Dropdown resolution;
    [SerializeField] TMP_Dropdown fullscreen;

    // Start is called before the first frame update
    void Start()
    {


        {
            resolution.ClearOptions();
            List<string> options = new List<string>();
            for (int i = 0; i < Screen.resolutions.Length; i++) {
                options.Add("" + Screen.resolutions[i].width + " x " + Screen.resolutions[i].height + " " + Screen.resolutions[i].refreshRate + "hz");
            }

            int currentResolution = PlayerPrefs.GetInt(resolution_PPrefsTag, -1);
            if (currentResolution == -1) {
                for (int i = 0; i < Screen.resolutions.Length; i++) {
                    if ((Screen.resolutions[i].width == Screen.currentResolution.width) &&
                         (Screen.resolutions[i].height == Screen.currentResolution.height) &&
                         (Screen.resolutions[i].refreshRate == Screen.currentResolution.refreshRate)) {
                        currentResolution = i;
                    }
                }
            }
            resolution.AddOptions(options);
            resolution.SetValueWithoutNotify(currentResolution);
        }


        {
            int fullscreenValue = PlayerPrefs.GetInt(fullscreen_PPrefsTag, -1);
            if (fullscreenValue != -1) {
                fullscreen.value = fullscreenValue;
            } else {
                fullscreen.SetValueWithoutNotify(Screen.fullScreen == true ? 0 : 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneLight.intensity = slider.value * 3;
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;

        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
    }
    

    public void OnSliMusicValue(float newValue) {
        PlayerPrefs.SetFloat(music_PPrefsTag, newValue);
        myMixer.SetFloat("MusicVolume", LinearToDecibel(newValue));
    }

    public void OnSliSFXValue(float newValue) {
        PlayerPrefs.SetFloat(sfx_PPrefsTag, newValue);
        myMixer.SetFloat("SFXVolume", LinearToDecibel(newValue));
    }

    float oldTimeScale = 0f;
    bool menuIsOpen = false;


    public static float LinearToDecibel(float linear) {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    public static float DecibelToLinear(float dB) {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }

    public float NormalizedToRange(float value, float min, float max) {
        float range = max - min;
        float rangedValue = min + (value * range);

        return rangedValue;
    }

    public void OnResolutionChange(int option) {
        PlayerPrefs.SetInt(resolution_PPrefsTag, option);
        ApplyResolution();
    }

    public void OnFullscreenChange(int option) {
        PlayerPrefs.SetInt(fullscreen_PPrefsTag, option);
        ApplyResolution();
    }
 
    void ApplyResolution() {
        Screen.SetResolution(
            Screen.resolutions[resolution.value].width,
            Screen.resolutions[resolution.value].height,
            fullscreen.value == 0,
            Screen.resolutions[resolution.value].refreshRate
        );
    }
}
