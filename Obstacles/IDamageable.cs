using System;
using Obstacles;
using UnityEngine;

namespace Obstacles
{
    public interface IDamageable
    {
        void Damage(float amount);
        void CollisionBehavior();
    }

    public enum BuffType
    {
        Damage,
        FireRate
    }
    
}

