using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AudioCueKey 
{
    public static AudioCueKey Invalid = new AudioCueKey(-1, null);

    internal int Value;
    internal AudioCueSO AudioCue;

    internal AudioCueKey(int value, AudioCueSO audioCue)
    {
        Value = value;
        AudioCue = audioCue;
    }

    public override bool Equals(object obj)
    {
        return obj is AudioCueKey x && Value == x.Value && AudioCue == x.AudioCue;
    }

    //Using XOR operator to generate hash code combing the fields
    //Allows type to work correctly in hash table
    public override int GetHashCode()
    {
        return Value.GetHashCode() ^ AudioCue.GetHashCode();
    }

    //Operator Override
    public static bool operator ==(AudioCueKey x, AudioCueKey y)
    {
        return x.Value == y.Value && x.AudioCue == y.AudioCue;
    }

    //Operator Override
    public static bool operator !=(AudioCueKey x, AudioCueKey y)
    {
        return !(x == y);
    }
}
