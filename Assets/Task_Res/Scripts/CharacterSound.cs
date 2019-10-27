using UnityEngine;

public class CharacterSound : Singleton<CharacterSound>
{
    private AudioSource audioSource;
    public AudioClip[] skillClips;
    public AudioClip damageClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnBaseSkill1Music()
    {
        audioSource.clip = skillClips[0];
        audioSource.Play();
    }
    void OnBaseSkill2Music()
    {
        audioSource.clip = skillClips[1];
        audioSource.Play();
    }
    void OnSkill1Music()
    {
        audioSource.clip = skillClips[2];
        audioSource.Play();
    }
    void OnSkill2Music()
    {
        audioSource.clip = skillClips[3];
        audioSource.Play();
    }
    void OnSkill3Music()
    {
        audioSource.clip = skillClips[4];
        audioSource.Play();
    }

    void OnFinalHitSkill1Music()
    {
        audioSource.clip = skillClips[5];
        audioSource.Play();
    }
    void OnFinalHitSkill2Music()
    {
        audioSource.clip = skillClips[6];
        audioSource.Play();
    }
    void OnFinalHitSkill3Music()
    {
        audioSource.clip = skillClips[7];
        audioSource.Play();
    }

    public void PlayDamageClip()
    {
        audioSource.clip = damageClip;
        if (!audioSource.isPlaying) audioSource.Play();
    }
}
