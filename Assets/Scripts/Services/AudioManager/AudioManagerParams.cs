using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[CreateAssetMenu(menuName="Audio/AudioManagerParams")]
public class AudioManagerParams : ScriptableObject
{
    public AudioMixer mixer;
    public AudioMixerGroup masterGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;
    public List<AudioMixerSnapshot> snapShots;
    public SoundEffectDictionary soundDictionary;
    public MusicDictionary musicDictionary;
}
