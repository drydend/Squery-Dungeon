using UnityEngine;

namespace Ability
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private float _cooldownTime;
        private Character _character;

        public abstract void Activate();
    }
}
