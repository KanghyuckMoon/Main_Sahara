
using System.Collections;
using System.Collections.Generic;
using UI.Production;
using UnityEngine;

namespace UI.Popup
{
    
    public class PopupTutorialData
    {
        public PopupTutorialData(PopupTutorialDataSO _dataSO)
        {
            titleAddress = _dataSO.titleAddress;
            detailAddress = _dataSO.detailAddress;
            detailImageAddress = _dataSO.detailImageAddress;

            detailImageAddressList = _dataSO.detailImageAddressList; 
        }
        public string titleAddress;
        public string detailAddress;
        public string detailImageAddress; 

        public List<string> detailImageAddressList = new List<string>();
    }

    [CreateAssetMenu(menuName = "SO/UI/PopupTutorialDataSO")]
    public class PopupTutorialDataSO : ScriptableObject, IObserble
    {
        private List<Observer> listeners = new List<Observer>();
        public List<Observer> Observers => listeners; 
        
        public string titleAddress;
        public string detailAddress;
        public string detailImageAddress;

        public List<string> detailImageAddressList = new List<string>(); 
        public void AddObserver(Observer _listener)
        {
            listeners.Add(_listener);
        }

        public void Send()
        {
            foreach (Observer _listener in listeners)
            {
                _listener.Receive();
            }
        }
    }

}
