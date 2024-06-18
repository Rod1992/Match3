using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Calculator;
using Entities;
using GridUi;

public class SceneInstance : MonoBehaviour
{
    [SerializeField]
    GemGridPanel panel;

    // Start is called before the first frame update
    void Start()
    {
        LoadGrid();
    }

    private void LoadGrid()
    {
        Gem[][] gemgrid = GridCalculator.GenerateGemGrid(8);

        panel.Load(gemgrid);
    }

    public void Reload()
    {
        panel.Clear();

        LoadGrid();
    }
}
