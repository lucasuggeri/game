using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Playsound : MonoBehaviour
{
    private Playsound playSound;
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

     }

    // Versão padrão (compatível com chamadas antigas)
    public void Play(int sound)
    {
        Play(sound, 1f); // volume padrão = 100%
    }

    // Versão com volume customizável
    public void Play(int sound, float volume)
    {
        Play(sound, volume, 1f, 1f); // sem variação de pitch
    }

    // Versão com volume e pitch aleatório
    public void Play(int sound, float volume, float minPitch, float maxPitch)
    {
        if (sound >= 0 && sound < audioClips.Count && audioClips[sound] != null)
        {
            if (sound >= 0 && sound < audioClips.Count && audioClips[sound] != null)
            {
                StopAllCoroutines();
                StartCoroutine(PlayWithPitch(audioClips[sound], volume, minPitch, maxPitch));
            }
            //else
            //{
            //    Debug.LogWarning("Som inválido ou não atribuído na lista de áudio!");
            //}
        }
        //else
        //{
        //    Debug.LogWarning("Som inválido ou não atribuído na lista de áudio!");
        //}
    }

    private IEnumerator PlayWithPitch(AudioClip clip, float volume, float minPitch, float maxPitch)
    {
        float originalPitch = audioSource.pitch;
        float randomPitch = Random.Range(minPitch, maxPitch);

        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(clip, volume);

        // tempo real considerando o pitch
        float duration = clip.length / randomPitch;

        yield return new WaitForSeconds(duration);

        audioSource.pitch = originalPitch;
    }
    public void SetupButtonSounds()
    {
        if (playSound == null) return;

        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include); // inclui inativos

        foreach (Button button in buttons)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = button.gameObject.AddComponent<EventTrigger>();

            if (trigger.triggers == null)
                trigger.triggers = new List<EventTrigger.Entry>();

            // Evita duplicar evento
            bool hasSelect = false;
            foreach (var entry in trigger.triggers)
            {
                if (entry.eventID == EventTriggerType.Select)
                {
                    hasSelect = true;
                    break;
                }
            }

            if (hasSelect) continue;

            EventTrigger.Entry selectEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Select
            };

            selectEntry.callback.AddListener((eventData) =>
            {
                playSound.Play(1, 1f, 0.95f, 1.05f);
            });

            trigger.triggers.Add(selectEntry);
        }
    }
}
