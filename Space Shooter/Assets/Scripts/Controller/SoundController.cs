using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSFXType
{
    ExpAst,
    ExpEnemy,
    ExpPlayer,
    FireEnemy,
    FirePlayer
}

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMClip, mEffectClip;
    // Start is called before the first frame update
    void Start()
    {
        // audio clip 로드
        // audio setting 로드
    }
    public void SetBGMVolume(float value)
    {
        mBGM.volume = value;
    }

    public void SetEffectVolume(float value)
    {
        mEffect.volume = value;
    }

    public void ChangeBGM(int index)
    {
        mBGM.Stop();
        mBGM.clip = mBGMClip[index];
        mBGM.Play();
    }
    public void PlayEffectSound(int index)
    {
        mEffect.PlayOneShot(mEffectClip[index]);
        //AudioSource.PlayClipAtPoint(mEffectClip[index], Vector3.zero);
    }
}
