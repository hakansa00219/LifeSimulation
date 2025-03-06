using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Life
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Particle : MonoBehaviour
    {
        private Transform[] _particles;
        private float[,] _attractionTable;
        public void Init(ref Transform[] particles, float[,] attractionTable)
        {
            _particles = particles;
            _attractionTable = attractionTable;
        }
        private void FixedUpdate()
        {
            // Debug.Log(_particles.Length);
            foreach (var particle in _particles)
            {
                if (transform == particle) return;
                
                float distance = Vector2.Distance(particle.position, transform.position);
                if (distance > 100f) return;

                float forcePower = _attractionTable[0, 0];
                
                // Debug.Log("Moving?");

                Vector2 particlePosition = particle.position;
                Vector2 movement = particlePosition - (Vector2)transform.position;
                
                particlePosition += movement * forcePower / distance * Time.deltaTime;

                particle.position = particlePosition;
            }
        }
    }
}