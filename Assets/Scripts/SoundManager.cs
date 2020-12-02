using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    Shoot, SpecialBreak, SummonSpecial, SummonBrick, BrickBreak, LevelUp
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource ShootSound;
    [SerializeField] private AudioSource SpecialBreakSound;
    [SerializeField] private AudioSource BrickBreakSound;
    [SerializeField] private AudioSource SummonSpecialSound;
    [SerializeField] private AudioSource SummonBrickSound;
    [SerializeField] private AudioSource LevelUpSound;

    [Space()]
    [SerializeField] private AudioSource PlayLoopSound;

    private void Awake()
    {
        PlayLoopSound.loop = true;
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.Shoot:
                ShootSound.Play();
                break;

            case Sounds.SpecialBreak:
                SpecialBreakSound.Play();
                break;

            case Sounds.SummonSpecial:
                SummonSpecialSound.Play();
                break;

            case Sounds.SummonBrick:
                SummonBrickSound.Play();
                break;

            case Sounds.BrickBreak:
                BrickBreakSound.Play();
                break;

            case Sounds.LevelUp:
                LevelUpSound.Play();
                break;

            default:
                break;
        }
    }
}
