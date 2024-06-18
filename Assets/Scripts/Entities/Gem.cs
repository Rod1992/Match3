using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public enum EDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public class Gem
    {
        GemConfig config;

        public Gem up;
        public Gem down;
        public Gem left;
        public Gem right;

        public GemConfig Config
        {
            get => config;
        }

        public Gem(GemConfig conf)
        {
            config = conf;
        }

        public Gem GetDirection(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.UP:
                    return up;
                case EDirection.DOWN:
                    return down;
                case EDirection.LEFT:
                    return left;
                case EDirection.RIGHT:
                    return right;
                default:
                    return null;
            }
        }

        public EDirection GetTurn(EDirection origin, EDirection twist)
        {
            switch (origin)
            {
                case EDirection.UP:
                    return origin;
                case EDirection.DOWN:
                    switch (twist)
                    {
                        case EDirection.UP:
                            return EDirection.DOWN;
                        case EDirection.DOWN:
                            return EDirection.UP;
                        case EDirection.LEFT:
                            return EDirection.RIGHT;
                        case EDirection.RIGHT:
                            return EDirection.LEFT;
                    }
                    break;
                case EDirection.LEFT:
                    switch (twist)
                    {
                        case EDirection.UP:
                            return EDirection.LEFT;
                        case EDirection.DOWN:
                            return EDirection.RIGHT;
                        case EDirection.LEFT:
                            return EDirection.UP;
                        case EDirection.RIGHT:
                            return EDirection.DOWN;
                    }
                    break;
                case EDirection.RIGHT:
                    switch (twist)
                    {
                        case EDirection.UP:
                            return EDirection.RIGHT;
                        case EDirection.DOWN:
                            return EDirection.LEFT;
                        case EDirection.LEFT:
                            return EDirection.DOWN;
                        case EDirection.RIGHT:
                            return EDirection.UP;
                    }
                    break;
            }

            return EDirection.UP;
        }

        /// <summary>
        /// Is there a solvable line in this direction?
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsDirectionSolved(EDirection direction)
        {
            Gem firstChild = this.GetDirection(direction);

            if (firstChild == null)
                return false;

            Gem secondChild = firstChild.GetDirection(direction);

            if (secondChild == null)
                return false;

            if (firstChild.Config.Id == this.config.Id && secondChild.Config.Id == this.config.Id)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set first found id not used by neighbours
        /// </summary>
        /// <param name="gemConfigs"></param>
        public void SetNotSolvable(List<GemConfig> gemConfigs)
        {
            HashSet<int> idsNeighbours = new HashSet<int>();

            if (up != null)
                idsNeighbours.Add(up.Config.Id);
            if (down != null)
                idsNeighbours.Add(down.Config.Id);
            if (left != null)
                idsNeighbours.Add(left.Config.Id);
            if (right != null)
                idsNeighbours.Add(right.Config.Id);

            for(int i = 0; i < gemConfigs.Count; i++)
            {
                if (!idsNeighbours.Contains(i))
                {
                    this.config = gemConfigs.Find((x) => x.Id == i);
                }
            }
        }

        /// <summary>
        /// returns possible solvable lines with movement
        /// if 0, not possible solvable lines
        /// </summary>
        /// <returns></returns>
        public int PossibleSolvableLines()
        {
            int lines = 0;

            if (IsPossibleSolvableLine(EDirection.UP))
                lines++;
            if (IsPossibleSolvableLine(EDirection.DOWN))
                lines++;
            if (IsPossibleSolvableLine(EDirection.LEFT))
                lines++;
            if (IsPossibleSolvableLine(EDirection.RIGHT))
                lines++;

            return lines;
        }

        private bool IsPossibleSolvableLine(EDirection direction)
        {
            Gem firstChild = this.GetDirection(direction);

            if (firstChild == null)
                return false;
            //case x-x
            //      o
            Gem leftFromFirstChild = firstChild.GetDirection(GetTurn(direction, EDirection.LEFT));
            Gem rightFromFirstChild = firstChild.GetDirection(GetTurn(direction, EDirection.RIGHT));

            if(leftFromFirstChild != null && rightFromFirstChild != null && leftFromFirstChild.config.Id == this.config.Id && rightFromFirstChild.config.Id == this.config.Id)
            {
                return true;
            }

            //case -xx, not done because I am too tired
            //     o

            //case o-xx
            Gem secondChild = firstChild.GetDirection(direction);

            if (secondChild != null && secondChild.config.Id == this.config.Id)
            {
                Gem thirdChild = secondChild.GetDirection(direction);
                if(thirdChild != null && thirdChild.config.Id == this.config.Id)
                {
                    return true;
                }
            }


            return false;
        }
    }
}
