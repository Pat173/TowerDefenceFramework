using System;
using GameFramework;
using System.Collections.Generic;

namespace AI_Strategy
{
    /*
     * very simple example strategy based on random placement of units.
     */
    public class GongiStrat : AbstractStrategy
    {
        private static Random random = new Random();
        private bool m_firstTurn = true;

        public GongiStrat(Player player) : base(player)
        {
            
        }

        /*
         * example strategy for deploying Towers based on random placement and budget.
         * Your one should be better!
         */
        public override void DeployTowers()
        {

            if (m_firstTurn)
            {
                player.TryBuyTower<Tower>(1, 5);
                player.TryBuyTower<Tower>(3, 5);
                player.TryBuyTower<Tower>(5, 5);
                m_firstTurn = false;
            }


            bool positioned = false;
            int count = 0;
            while (!positioned && count < 20)
            {
                count++;
                int x = random.Next(PlayerLane.WIDTH);
                int y = random.Next(PlayerLane.HEIGHT - PlayerLane.HEIGHT_OF_SAFETY_ZONE) + PlayerLane.HEIGHT_OF_SAFETY_ZONE;
                if (player.HomeLane.GetCellAt(x, y).Unit == null)
                {
                    positioned = true;
                    player.TryBuyTower<Tower>(x, y);
                }
            }
        }

        /*
         * example strategy for deploying Soldiers based on random placement and budget.
         * Yours should be better!
         */
        public override void DeploySoldiers()
        {
            if (m_firstTurn) return;

            if (player.Gold > 15)
            {
                player.TryBuySoldier<MySoldier>(6);
                player.TryBuySoldier<MySoldier>(5);
                player.TryBuySoldier<MySoldier>(4);
                return;
            }

            if (player.Gold > 30)
            {
                player.TryBuySoldier<MySoldier>(0);
                player.TryBuySoldier<MySoldier>(1);
                player.TryBuySoldier<MySoldier>(2);
                return;
            }

            if (player.Gold > 50)
            {
                player.TryBuySoldier<MySoldier>(3);
                return;
            }
        }

        /*
         * called by the game play environment. The order in which the array is returned here is
         * the order in which soldiers will plan and perform their movement.
         *
         * The default implementation does not change the order. Do better!
         */
        public override List<Soldier> SortedSoldierArray(List<Soldier> unsortedList)
        {
            return unsortedList;
        }

        /*
         * called by the game play environment. The order in which the array is returned here is
         * the order in which towers will plan and perform their action.
         *
         * The default implementation does not change the order. Do better!
         */
        public override List<Tower> SortedTowerArray(List<Tower> unsortedList)

        {
            return unsortedList;
        }
    }
}
