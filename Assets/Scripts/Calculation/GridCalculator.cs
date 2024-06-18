using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Calculator
{
    public static class GridCalculator
    {
        /// <summary>
        /// Generate Gem Grid of size x size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Gem[][] GenerateGemGrid(int size)
        {
            List<GemConfig> gemConfigs = GemConfig.GetGemConfigs();

            if (gemConfigs.Count < 1)
            {
                throw new System.Exception("No gem configs loaded");
            }

            int posSolvLines = 0;

            Gem[][] grid = new Gem[size][];
            do
            {
                InitRandom(size, gemConfigs, grid);
                VerifyNotSolvedLines(size, gemConfigs, grid);
                posSolvLines = GetPosLines(size, grid);
                Debug.Log("Pos lines to solve are : " + posSolvLines);
            } while (posSolvLines < 3);

            return grid;
        }

        private static int GetPosLines(int size, Gem[][] grid)
        {
            int posSolvLines = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    posSolvLines += grid[i][j].PossibleSolvableLines();

                }
            }

            return posSolvLines;
        }

        /// <summary>
        /// Make sure we don't have solved lines
        /// </summary>
        /// <param name="size"></param>
        /// <param name="gemConfigs"></param>
        /// <param name="grid"></param>
        private static void VerifyNotSolvedLines(int size, List<GemConfig> gemConfigs, Gem[][] grid)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i][j].IsDirectionSolved(EDirection.RIGHT) || grid[i][j].IsDirectionSolved(EDirection.DOWN))
                    {
                        grid[i][j].SetNotSolvable(gemConfigs);
                    }
                }
            }
        }

        /// <summary>
        /// Setup of the grid array and neighbour values
        /// </summary>
        /// <param name="size"></param>
        /// <param name="gemConfigs"></param>
        /// <param name="grid"></param>
        private static void InitRandom(int size, List<GemConfig> gemConfigs, Gem[][] grid)
        {
            int randIndex = 0;
            for (int i = 0; i < size; i++)
            {
                grid[i] = new Gem[size];
                for (int j = 0; j < size; j++)
                {
                    randIndex = Random.Range(0, gemConfigs.Count);
                    grid[i][j] = new Gem(gemConfigs[randIndex]);

                    if(i > 0)//set up
                    {
                        grid[i][j].up = grid[i - 1][j];
                        grid[i - 1][j].down = grid[i][j];
                    }

                    if(j > 0)//set left
                    {
                        grid[i][j].left = grid[i][j - 1].up;
                        grid[i][j - 1].right = grid[i][j];
                    }
                }
            }
        }
    }
}
