using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new Enemy", menuName ="Create New Enemy")]
public class Enemy_SO : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public int enemySpeed;
    public AudioClip tauntSound;
    public AudioClip painSound;
    public AudioClip deathSound;
    public GameObject projectileOrSummon;

}
