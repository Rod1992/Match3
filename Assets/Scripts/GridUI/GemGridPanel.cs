using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace GridUi
{
    public class GemGridPanel : MonoBehaviour
    {
        [SerializeField]
        float marginX;
        [SerializeField]
        float marginY;

        List<GemMono> poolGemMonos = new List<GemMono>();

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Load(Gem[][] gems)
        {
            bool pooled = poolGemMonos.Count > 0;
            GemMono prefab = Resources.Load<GemMono>("Prefabs/PrefabGemMono");
            GemMono gemMono;
            float posX;
            float posY = this.transform.position.y;
            foreach (Gem[] gemrow in gems)
            {
                posX = this.transform.position.x;
                foreach (Gem gem in gemrow)
                {
                    if (!pooled)
                    {
                        gemMono = Instantiate<GemMono>(prefab, this.transform);
                        poolGemMonos.Add(gemMono);
                    }
                    else
                    {
                        gemMono = poolGemMonos.Find((x) => !x.gameObject.activeSelf);// not ideal for a pooling system, but it serves the purpose of the test
                        gemMono.gameObject.SetActive(true);
                    }   

                    int X = (int)(posX - this.transform.position.x) / (int)marginX;
                    int Y = (int)(posY - this.transform.position.y) / (int)marginY;

                    gemMono.Init(Y, X, gem);
                    gemMono.transform.position = new Vector3(posX, posY);
                    posX += marginX;
                }
                posY += marginY;
            }
        }

        public void Clear()
        {
            foreach (GemMono gemMono in poolGemMonos)
            {
                gemMono.gameObject.SetActive(false);
            }
        }
    }
}

