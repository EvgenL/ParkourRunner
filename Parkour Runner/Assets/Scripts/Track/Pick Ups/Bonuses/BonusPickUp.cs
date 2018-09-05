using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Managers;
using Assets.Scripts.Pick_Ups.Effects;

namespace Assets.Scripts.Pick_Ups
{
    public class BonusPickUp : PickUp
    {
        public BonusName BonusName;

        protected override void Pick()
        {
            GameManager.Instance.AddBonus(BonusName);
            Destroy(gameObject);
        }
    }
}
