using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    [System.Serializable]
    public partial struct EntityBaseData
    {
        #region Database fields
        [SerializeField]
        //[HideInInspector]
        private uint _id;
        public uint ID { get { return _id; } set { _id = value; } }

        [SerializeField]
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        [SerializeField]
        private string _description;
        public string Description { get { return _description; } set { _description = value; } }

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon { get { return _icon; } set { _icon = value; } }
        #endregion
    }
}
