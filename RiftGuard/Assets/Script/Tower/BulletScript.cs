using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject target;
    private float speed = 10f;
    private float damage;

    [HideInInspector] public bool piercing = false;

    [SerializeField] private GameObject soundBullet;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip hitSound;

    [SerializeField] private float shootSoundVolume;
    [SerializeField] private float hitSoundVolume;

    private void Start()
    {
        GameObject sound = Instantiate(soundBullet,transform.position,transform.rotation);
        if (!SoundController.MuteSound)
        {
            sound.GetComponent<AudioSource>().volume = shootSoundVolume;
            sound.GetComponent<AudioSource>().clip = shootSound;
        }
    }

    private void Update()
    {
        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            if(target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector2 dir = target.transform.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if(dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

    }

    private void HitTarget()
    {
        if (piercing)
        {
            target.GetComponent<EnemyControl>().PierceHit(-damage);
        }
        else
        { 
            target.GetComponent<EnemyControl>().AddHealth(-damage);
        }

        if (!SoundController.MuteSound)
        {
            GameObject sound = Instantiate(soundBullet, transform.position, transform.rotation);
            sound.GetComponent<AudioSource>().volume = hitSoundVolume;
            sound.GetComponent<AudioSource>().clip = hitSound;
        }
        Destroy(gameObject);
    }


    // Other Functions used to SET/GET value
    public void Seek(GameObject _target)
    {
        target = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
