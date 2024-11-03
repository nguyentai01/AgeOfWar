using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager ins;
    public static AudioManager Ins { get => ins; private set { } }

   public AudioSource Mussic;
   public AudioSource Sfx;

   public AudioClip bg_battle;
   public AudioClip bg_mussic;
   public AudioClip snd_coin_drop;
   public AudioClip snd_Click;
   
   public AudioClip[] sndCavs;
   public AudioClip[] snd_Infs;
   public AudioClip[] sndRangs;

    private float sfx, music;

    private void Awake()
    {
        ins = this;

        sfx = Pref.GetSfx();
        music = Pref.GetMusic();
        Mussic.volume = music;

    }

    public void SetAuSfx()
    {
        sfx = Pref.GetSfx();
    }

    public void SetAuMusic(float au)
    {
        music = au;
        Mussic.volume = music;
        SetAuSfx();
    }

    public void SetMusic(AudioClip au,float voloum =1)
    {
        Mussic.clip = au;
        Mussic.volume = music;
        Mussic.Play();
    }

    public void SetSfx(AudioClip au, float voloum = 1)
    {
        Sfx.PlayOneShot(au, sfx);
    }

    public void SetAudioAttack(TypeTroops troop,int age, float voloum =1)
    {
        switch (troop)
        {
            case TypeTroops.Inf:
                SetSfx(snd_Infs[age], voloum);
                break;
            case TypeTroops.Cav:
                SetSfx(sndCavs[age], voloum);

                break;
            case TypeTroops.Rang:
                SetSfx(sndRangs[age], voloum);
                break;
        }
    }
}
