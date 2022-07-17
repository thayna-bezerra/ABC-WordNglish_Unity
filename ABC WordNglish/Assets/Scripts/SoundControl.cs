using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl sounds;


    [Space(10)]

    [Header("Efeitos Sonoros")]
    public AudioSource somPulo;
    public AudioSource somColectedOthers;
    public AudioSource somColectedCoins;
    public AudioSource somDanoNoPlayer;
    public AudioSource somDanoNoInimigo;

    private void Awake()
    {
        sounds = this;

    } //inicializar //instanciar classe

}
