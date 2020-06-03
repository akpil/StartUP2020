using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private const string MIXER_MASTER = "MixerMaster";
    private const string MIXER_BGM = "MixerBGM";
    private const string MIXER_FX = "MixerEffect";
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMArr, mEffectArr;
    [SerializeField]
    private AudioMixer mMixer;

    public float MasterVolume
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_MASTER, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            mMixer.SetFloat(MIXER_MASTER, vol);
        }
    }

    public float BGMVolume
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_BGM, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            mMixer.SetFloat(MIXER_BGM, vol);
        }
    }

    public float EffectVolume
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_FX, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            mMixer.SetFloat(MIXER_FX, vol);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            mEffect.PlayOneShot(mEffectArr[0]);
        }
    }
}
