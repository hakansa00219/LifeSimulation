using System.Threading;
using Life.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Life
{
    public class ParticleSpawnerMultithread : MonoBehaviour
    {
        [SerializeField, Required] private SimulationSettings simulationSettings;
        [SerializeField, Required] private ParticlesAttraction particlesAttraction;
        [SerializeField, Required] private Transform particlePrefab;

        private ParticleData[] _particleData;
        private Vector2[] _movements;
        private Thread[] _threads;
        private Transform[] _particles;
        private float _forcePower;
        private void Start()
        {
            SpawnParticles();

            _forcePower = particlesAttraction.AttractionTable[0, 0];
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            
            for (int i = 0; i < _particles.Length; i++)
            {
                _particleData[i] = new ParticleData()
                {
                    Position = _particles[i].position,
                };
            }

            
            
            for (var i = 0; i < _particleData.Length; i++)
            {
                int index = i;
                _threads[index] = new Thread(() =>
                {
                    ParticleData particleData = _particleData[index];
                    Vector2 particleMovement = Vector2.zero;

                    for (int j = 0; j < _particleData.Length; j++)
                    {
                        if(index == j) continue;
                        
                        Vector2 particle2Position = _particleData[j].Position;
                        float distance = Vector2.Distance(particleData.Position, particle2Position);

                        if (distance <= 2f)
                        {
                            particleMovement += (particleData.Position - particle2Position) * _forcePower / distance *
                                                deltaTime;
                        }
                    }

                    _movements[index] = particleMovement;
                    
                });
                _threads[index].Start();
            }
            
            foreach (var thread in _threads)
            {
                thread.Join();
            }
            
            for (var i = 0; i < _particles.Length; i++)
            {
                _particles[i].position += (Vector3)_movements[i];
            }
        }
        

        private void SpawnParticles()
        {
            int particleAmount = simulationSettings.ParticleAmount;
            float particleSize = simulationSettings.ParticleSize;
            float particleSizeOffset = particleSize / 2f;
            Vector2 xBounds = simulationSettings.xBounds;
            Vector2 yBounds = simulationSettings.yBounds;

            _particles = new Transform[particleAmount];
            _threads = new Thread[particleAmount];
            _movements = new Vector2[particleAmount];
            _particleData = new ParticleData[particleAmount];
            
            for (int i = 0; i < particleAmount; i++)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(xBounds.x + particleSizeOffset, xBounds.y - particleSizeOffset),
                    Random.Range(yBounds.x + particleSizeOffset, yBounds.y - particleSizeOffset));

                _particles[i] = Instantiate(particlePrefab, randomPosition, Quaternion.identity);
                // particle.Init(ref particles, _particlesAttraction.AttractionTable);
            }
        }
        
        private struct ParticleData
        {
            public Vector2 Position;
        }
    }

    
}


