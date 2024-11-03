using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUi : MonoBehaviour
{
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Image vibImg;
    [SerializeField] private Image BgVib;

    [SerializeField] private RectTransform poinVib;

    [SerializeField] private Sprite[] listSpriteVibs;
    [SerializeField] private Sprite[] listSpriteBgVibs;

    private bool isTurn = true;

    private float sfx, music;
    private int vib;

    private void OnEnable()
    {
        sfx = Pref.GetSfx();
        music = Pref.GetMusic();
        vib = Pref.GetVib();
        SetVib();

    }
    public void SoundSetting()
    {
        sfx = sliderSFX.value;
    }

    public void MussicSetting()
    {
        music = sliderMusic.value;
        AudioManager.Ins.SetAuMusic(music);
    }

    public void Btn_TurnVib()
    {

        isTurn = !isTurn;
        float rang = poinVib.anchoredPosition.x * -1;
        poinVib.DOLocalMove(new Vector2(rang, poinVib.anchoredPosition.y), .2f).OnComplete(() =>
        {
            vibImg.sprite = isTurn ? listSpriteVibs[0] : listSpriteVibs[1];
            BgVib.sprite = isTurn ? listSpriteBgVibs[0] : listSpriteBgVibs[1];
            vib = isTurn ? 1 : 0;
            Pref.SetVib(vib);
        });
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }

    private void SetVib()
    {
        isTurn = vib == 1;
        float rang = Mathf.Abs(poinVib.anchoredPosition.x);
        if(isTurn)
        {
            poinVib.anchoredPosition = new Vector2(rang, poinVib.anchoredPosition.y);
        }
        else
        {
            poinVib.anchoredPosition = new Vector2(-rang, poinVib.anchoredPosition.y);
        }
        vibImg.sprite = isTurn ? listSpriteVibs[0] : listSpriteVibs[1];
        BgVib.sprite = isTurn ? listSpriteBgVibs[0] : listSpriteBgVibs[1];

        sliderSFX.value = sfx;
        sliderMusic.value = music;

    }

    public void Btn_Closed()
    {
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Pref.SetSfx(sfx);
        Pref.SetMusic(music);
        AudioManager.Ins.SetAuMusic(music);
    }
}
