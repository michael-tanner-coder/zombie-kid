using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicTrack", menuName = "Audio/New Music Track")]
    public class MusicTrack : ScriptableObject
    {
        #region config
        [SerializeField] private AudioClip clip;
        public AudioClip Clip => clip;

        [SerializeField] private bool loop;
        public bool Loop => loop;

        [Range(0f, 1f)]
        public float volume = 1f;

        [Range(-3f, 3f)]
        public float pitch = 1f;

        private AudioSource source;
        #endregion


        public void Play()
        {
            if (clip == null)
            {
                Debug.Log($"Missing sound clip for {name}");
            }

            if (source == null)
            {
                var _obj = new GameObject("Sound", typeof(AudioSource));
                source = _obj.GetComponent<AudioSource>();
            }

            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;

            source.Play();   
        }

        public void Stop()
        {
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }

        public void Pause()
        {
            source.Pause();
        }
    }
