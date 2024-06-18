using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "GemConfig", menuName = "ScriptableObjects/GemConfig", order = 1)]
    public class GemConfig : ScriptableObject
    {
        [SerializeField]
        int id;
        [SerializeField]
        Sprite image;

        public int Id
        {
            get => id;
        }

        public Sprite Image
        {
            get => image;
        }

        public static List<GemConfig> GetGemConfigs()
        {
            List<GemConfig> configs = new List<GemConfig>();
            configs.AddRange(Resources.LoadAll<GemConfig>(""));
            return configs;
        }
    }
}
