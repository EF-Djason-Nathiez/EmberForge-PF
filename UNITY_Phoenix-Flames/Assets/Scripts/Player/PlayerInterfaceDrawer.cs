using System;
using Player;

[Serializable] public class PlayerInterfaceDrawer : PlayerComponent
{
   public override void Initialize(PlayerManager pm)
   {
      base.Initialize(pm);
      
     UpdatePlayerStateInformation();
   }

   public void UpdatePlayerStateInformation()
   {
       InterfaceManager.instance.SetHealthText($"Health : {manager.CurrentHealth()} <br>" +
                                               $"Speed : {manager.CurrentSpeed()} <br>");
   }
}

//Demain matin 07h00 sur place -
