using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    internal static object transform;
    [Range(1,11)]
    public int partCount = 11;

    [Range(0,11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName ="New Stage")]
public class Stage : ScriptableObject
{
    
    public Color stageBackgroundColor = Color.white;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.white;

    public List<Level> Levels = new List<Level>();
    internal object levels;
}
