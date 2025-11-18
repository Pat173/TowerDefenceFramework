using GameFramework;
using System.Collections.Generic;
namespace AI_Strategy
{
    /*
     * This class derives from Soldier and provides a new move method. Your assignment should
     * do the same - but with your own movement strategy.
     */
    public class GongiRoman : Soldier
    {
        private int m_retreatMovesLeft = 99;      // limit backwards dodges

        public override void Move()
        {
            // RETREAT LOGIC
            if (m_retreatMovesLeft > 0 && Health <= 4)
            {
                if (TryRetreat()) return;
            }

            if (speed > 0 && posY < PlayerLane.HEIGHT)
            {
                int x = posX;
                int y = posY;
                for (int i = speed; i > 0; i--)
                {
                    if (MoveTo(x, y + i)) return;
                    if (MoveTo(x + i, y + i)) return;
                    if (MoveTo(x - i, y + i)) return;
                    if (MoveTo(x + i, y)) return;
                    if (MoveTo(x - i, y)) return;
                    if (MoveTo(x, y - i)) return;
                    if (MoveTo(x - i, y - i)) return;
                    if (MoveTo(x + i, y - i)) return;
                }
            }
        }

        private bool TryRetreat()
        {

            for (int i = m_retreatMovesLeft; i > 0; i--)
            {
                int x = posX;
                int y = posY;

                if (m_retreatMovesLeft == 99)
                {
                    if (MoveTo(x + 1, y - 1))
                    {
                        m_retreatMovesLeft--;
                        return true;
                    }
                    if (MoveTo(x - 1, y - 1))
                    {
                        m_retreatMovesLeft--;
                        return true;
                    }
                }
                else
                {
                    if (MoveTo(x, y - 1))
                    {
                        m_retreatMovesLeft--;
                        return true;
                    }
                }

            }

            return false;
        }

        protected override List<Unit> SortTargetsInRange(List<Unit> targets)
        {
            // As you already had: attack highest HP first
            targets.Sort((a, b) => b.Health.CompareTo(a.Health));
            return targets;
        }
    }
}
