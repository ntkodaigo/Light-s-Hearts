using UnityEngine;

namespace Enigma.LightsHearts
{
    public interface ITakeDamage
    {
        void TakeDamage(int damage, Vector3 force);
    }
}