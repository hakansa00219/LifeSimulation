using System;
using System.Collections.Generic;
using System.Threading;
using Life.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Life
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField, Required] private SimulationSettings simulationSettings;
        [SerializeField, Required] private ParticlesAttraction _particlesAttraction;
        [SerializeField, Required] private Transform particlePrefab;

        private Transform[] _particles;
        private float _forcePower;
        private void Start()
        {
            SpawnParticles();

            _forcePower = _particlesAttraction.AttractionTable[0, 0];
        }

        private void LateUpdate()
        {
            foreach (var particle in _particles)
            {
                ParticleUpdate(particle);
            }
        }

        private void ParticleUpdate(Transform particle)
        {
            Vector2 particlePosition = particle.position;
            Vector2 particleMovement = Vector2.zero;
            
            foreach(var particle2 in _particles)
            {
                Vector2 particle2Position = particle2.position;
                
                if (particle == particle2) continue;
                
                float distance = Vector2.Distance(particlePosition, particle2Position);
                
                if (distance > 2f) continue;

                particleMovement += (particlePosition - particle2Position) * _forcePower / distance *
                                    Time.deltaTime;
            }

            particle.position = (Vector2)particle.position + particleMovement;
        }

        private void SpawnParticles()
        {
            int particleAmount = simulationSettings.ParticleAmount;
            float particleSize = simulationSettings.ParticleSize;
            float particleSizeOffset = particleSize / 2f;
            Vector2 xBounds = simulationSettings.xBounds;
            Vector2 yBounds = simulationSettings.yBounds;

            _particles = new Transform[particleAmount];
            
            for (int i = 0; i < particleAmount; i++)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(xBounds.x + particleSizeOffset, xBounds.y - particleSizeOffset),
                    Random.Range(yBounds.x + particleSizeOffset, yBounds.y - particleSizeOffset));

                _particles[i] = Instantiate(particlePrefab, randomPosition, Quaternion.identity);
                // particle.Init(ref particles, _particlesAttraction.AttractionTable);
            }
        }
    }

    
}


