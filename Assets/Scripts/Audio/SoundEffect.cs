using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        #region config

        private static readonly float SEMITONES_TO_PITCH_CONVERSION_UNIT = 1.05946f;

        public AudioClip[] clips;

        [Range(0f, 1f)]
        public float volume = 1f;

        //Pitch / Semitones
        public bool useSemitones;
        
        [Range(-10, 10)]
        public int semitones;
        private int minSemiTones = -10;

        [Range(-3f, 3f)]
        public float pitch = 1f;

        [SerializeField] private SoundClipPlayOrder playOrder;

        [SerializeField]
        private int playIndex = 0;

        #endregion

        public void SyncPitchAndSemitones()
        {
            if (useSemitones)
            {
                pitch = Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, semitones);
            }
            else
            {
                semitones = Mathf.RoundToInt(Mathf.Log10(pitch) / Mathf.Log10(SEMITONES_TO_PITCH_CONVERSION_UNIT));
            }
        }

        public AudioClip GetAudioClip()
        {
            // get current clip
            var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

            // find next clip
            switch (playOrder)
            {
                case SoundClipPlayOrder.in_order:
                    playIndex = (playIndex + 1) % clips.Length;
                    break;
                case SoundClipPlayOrder.random:
                    playIndex = Random.Range(0, clips.Length);
                    break;
                case SoundClipPlayOrder.reverse:
                    playIndex = (playIndex + clips.Length - 1) % clips.Length;
                    break;
            }

            // return clip
            return clip;
        }

        public void Play()
        {
            if (clips.Length == 0)
            {
                Debug.Log($"Missing sound clips for {name}");
            }

            var _obj = new GameObject("Sound", typeof(AudioSource));
            var source = _obj.GetComponent<AudioSource>();

            // set source config:
            source.clip = GetAudioClip();
            source.volume = volume;
            source.pitch = useSemitones
                ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, Random.Range(minSemiTones, semitones))
                : pitch;

            source.Play();
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }

        enum SoundClipPlayOrder
        {
            random,
            in_order,
            reverse
        }
    }
