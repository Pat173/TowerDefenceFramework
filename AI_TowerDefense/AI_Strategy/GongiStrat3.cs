using System;
using GameFramework;
using System.Collections.Generic;

namespace AI_Strategy
{
    /*
     * very simple example strategy based on random placement of units.
     */
    public class GongiStrat3 : AbstractStrategy
    {
        private static Random random = new Random();
        private bool m_firstTurn = true;

        private bool m_sendingAttack;
        private int m_attackRound = 0;

        private int[,] m_currentFormation;

        public GongiStrat3(Player player, TowerFormationType formation) : base(player)
        {
            m_currentFormation = formation == TowerFormationType.O_Shape ? O_Formation : H_Formation;
        }

        public enum TowerFormationType
        {
            O_Shape,
            H_Shape,
        }

        public static int[,] O_Formation = new int[,]
        {
            {1,8},
            {3,8},
            {5,8},
            {0,7},
            {2,7},
            {4,7},
            {6,7},
            {1,6},
            {3,6},
            {5,6},
        };

        public static int[,] H_Formation = new int[,]
        {
            {0,8},
            {2,8},
            {4,8},
            {6,8},
            {1,7},
            {3,7},
            {5,7},
            {0,6},
            {2,6},
            {4,6},
            {6,6},
        };

        /*
         * example strategy for deploying Towers based on random placement and budget.
         * Your one should be better!
         */
        public override void DeployTowers()
        {
            //if (player.HomeLane.TowerCount() >= 10) return;

            for (int i = 0; i < m_currentFormation.GetLength(0); i++)
            {

                int x = m_currentFormation[i, 0];
                int y = m_currentFormation[i, 1];
                player.TryBuyTower<GongiTower>(x, y);
            }

        }

        public override void DeploySoldiers()
        {
            if (player.Gold > 36 && !m_sendingAttack)
            {
                m_sendingAttack = true;
                m_attackRound = 0;
            }

            if (m_attackRound > 3)
            {
                m_sendingAttack = false;
                return;
            }

            if (m_sendingAttack)
            {

                for (int i = 0; i < 7; i++)
                {
                    player.TryBuySoldier<GongiSoldier>(i); 
                }
                m_attackRound++;

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
