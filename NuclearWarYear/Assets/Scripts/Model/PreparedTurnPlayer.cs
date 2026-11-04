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
            Debug.Log("0600  country   L  = " + EnergyCard);
            EnergyCard -=cost;
            Debug.Log("0602   N "+ EnergyCard + " message incident = " + cost);
        }
        public int GetEnergyCard()
        {
            return EnergyCard;
        }
    }
}