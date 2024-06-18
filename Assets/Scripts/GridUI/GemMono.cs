using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace GridUi
{
    public class GemMono : MonoBehaviour
    {
        [SerializeField]
        int X;
        [SerializeField]
        int Y;
        Gem gem;

        [SerializeField]
        SpriteRenderer spriteRenderer;


        public void Init(int x, int y, Gem gem)
        {
            X = x;
            Y = y;
            this.gem = gem;

            spriteRenderer.sprite = gem.Config.Image;
        }
    }
}
