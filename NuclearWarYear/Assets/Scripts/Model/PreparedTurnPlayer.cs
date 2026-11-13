using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PreparedTurnPlayer 
    {
        public int EnergyCard {  get; private set; }
        public PreparedTurnPlayer(int energyCard)
        {
            EnergyCard = energyCard;
        }
        public void ChangeEnergyCard(int cost)
        {
            
            EnergyCard -=cost;
            
        }
        public int GetEnergyCard()
        {
            return EnergyCard;
        }
    }
}