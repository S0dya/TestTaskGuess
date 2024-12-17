using UnityEngine;

namespace Game.UI
{
    public class UIParticleEffects : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _guessedRightParticleSystem;

        public void PlayGuessedRightParticleSystem(Vector2 position)
        {
            _guessedRightParticleSystem.transform.position = position;
            _guessedRightParticleSystem.Play();
        }

        public void ClearParticles()
        {
            _guessedRightParticleSystem.Clear();
        }
    }
}
