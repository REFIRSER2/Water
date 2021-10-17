using System.Collections;
using UnityEngine;

namespace Object.Base
{
    public class TeamBase : TeamObject
    {
        public override void OnInit()
        {
            base.OnInit();
            ResetHealth();
        }

        public override void OnThink()
        {
            base.OnThink();
        }
    }
}
