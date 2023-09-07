using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;
using TimeManager;

namespace CondinedModule
{
    public class RestorePlayer : RestoreTarget
    {
        public void RestorePlayerObj()
        {
            var _playerModule = PlayerObj.Player.GetComponent<AbMainModule>();
            _playerModule.transform.position = new Vector3(356f, 2f, 67.6f);// _playerModule.lastGroundPos;
            Restore(_playerModule);
            StaticTime.EntierTime = 1f;
            UI.Manager.UIManager.Instance.ActiveCursor(false);
        }
    }

}