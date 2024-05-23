using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
[Serializable]
public class WordSlot
{
    public Word[] Word;
}

[Serializable]
public class Word
{
    public string Spell;
    public string Korean;
    public int Number;
    public string Day;
    public bool State;
}