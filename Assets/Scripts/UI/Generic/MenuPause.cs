using UnityEngine;

namespace dnSR_Coding
{
    ///<summary> This class manages all the logic connected to the menu pause used in game. <summary>
    public class MenuPause : DefaultUIWindow
    {
        #region Enable, Disable

        protected override void OnEnable() 
        {
            base.OnEnable();

            GameManager.OnGameResumed += SendNotificationToUpdate;
            GameManager.OnGamePaused += SendNotificationToUpdate;
        }

        protected override void OnDisable() 
        {
            base.OnDisable();

            GameManager.OnGameResumed -= SendNotificationToUpdate;
            GameManager.OnGamePaused -= SendNotificationToUpdate;
        }

        #endregion
    }
}