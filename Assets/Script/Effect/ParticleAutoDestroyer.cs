using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(particle.isPlaying == false)  // 파티클이 재생 중이 아니라면 삭제
        {
            Destroy(gameObject);
        }
    }
}
