using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MotoresDeJogos
{
    public class AudioManager : GameComponent
    {
        private static AudioManager audioManager = null;
        
        private FileInfo[] audioFileList;
        
        private static Dictionary<string, SoundEffect> soundList;

        /// <summary>
        /// Constructs the manager for audio playback of all sound effects.
        /// </summary>
        /// <param name="game">The game that this component will be attached to.</param>
        /// <param name="audioFolder">The directory containing audio files.</param>
        private AudioManager(Game game) : base(game)
        {
            try
            {
                string[] subdirectoryEntries = Directory.GetDirectories("Content\\songs");
                soundList = new Dictionary<string, SoundEffect>();
                foreach (string s in subdirectoryEntries)
                {
                    DirectoryInfo info = new DirectoryInfo(s);
                    audioFileList = info.GetFiles("*.xnb");

                    for (int i = 0; i < audioFileList.Length; i++)
                    {
                        string soundName = Path.GetFileNameWithoutExtension(audioFileList[i].Name);
                        soundList[soundName] = game.Content.Load<SoundEffect>("songs\\" + audioFileList[i].Directory.Name + "\\" + soundName);
                        soundList[soundName].Name = soundName;
                    }
                }
            }
            catch (NoAudioHardwareException)
            {
                // silently fall back to silence
            }
        }

        public static void Initialize(Game game)
        {
            if (game == null)
                return;

            audioManager = new AudioManager(game);
            game.Components.Add(audioManager);
        }

        /// <summary>
        /// Plays a fire-and-forget sound effect by name.
        /// </summary>
        /// <param name="soundName">The name of the sound to play.</param>
        public static void PlaySoundEffect(string soundName)
        {
            if (audioManager == null || soundList == null)
                return;

            if (soundList.ContainsKey(soundName))
            {
                soundList[soundName].Play();
            }
        }

        /// <summary>
        /// Plays a sound effect by name and returns an instance of that sound.
        /// </summary>
        /// <param name="soundName">The name of the sound to play.</param>
        /// <param name="looped">True if sound effect should loop.</param>
        /// <param name="instance">The SoundEffectInstance created for this sound effect.</param>
        public static void PlaySoundEffect(string soundName, bool looped, out SoundEffectInstance instance)
        {
            instance = null;

            
            if (audioManager == null || soundList == null)
                return;

            if (soundList.ContainsKey(soundName))
            {
                try
                {
                    instance = soundList[soundName].CreateInstance();
                    if (instance != null)
                    {
                        instance.IsLooped = looped;
                        instance.Play();
                    }
                }
                catch (InstancePlayLimitException)
                {
                    // silently fail (returns null instance) if instance limit reached
                }
            }
        }
    }
}
