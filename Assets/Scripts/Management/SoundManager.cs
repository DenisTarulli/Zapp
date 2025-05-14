using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip coinClip;
    public AudioClip damageClip;
    public AudioClip deathClip;
    public AudioClip moveClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayCoin() => PlaySFX(coinClip);
    public void PlayDamage() => PlaySFX(damageClip);
    public void PlayDeath() => PlaySFX(deathClip);
    public void PlayMove() => PlaySFX(moveClip);
    private void OnEnable()
    {
        Coin.OnCoinCollected += PlayCoin;
        PlayerHealth.OnPlayerDamaged += PlayDamage;
        PlayerHealth.OnPlayerDied += PlayDeath;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= PlayCoin;
        PlayerHealth.OnPlayerDamaged -= PlayDamage;
        PlayerHealth.OnPlayerDied -= PlayDeath;
    }

}
