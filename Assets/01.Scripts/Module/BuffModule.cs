using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buff;

namespace Module
{
    public enum Bufftype
    {
        Update,
        Once,
        None
    }

    public class BuffModule : AbBaseModule,Obserble
    {
        public Dictionary<IBuff, Bufftype> buffDic = new Dictionary<IBuff, Bufftype>();
        public List<AbBuffEffect> buffList = new List<AbBuffEffect>();
        private List<Observer> observers = new List<Observer>();
            
        public List<Observer> Observers => observers;

        public BuffModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            AddBuff(new Healing_Buf(this).SetValue(10).SetDuration(10).SetPeriod(2).SetSpownObjectName("HealEffect").SetSprite("Demon"), Bufftype.Update);
        }

        public void AddBuff(AbBuffEffect _buff, Bufftype _bufftype)
        {
            buffDic.Add(_buff, _bufftype);
            buffList.Add(_buff);

            if (_bufftype == Bufftype.Once)
            {
                _buff.Buff(mainModule);
                buffDic.Remove(_buff);
                buffList.Remove(_buff);
            }
            Send(); // UI에 신호 보내기, 
        }

        public override void Update()
        {
            foreach(IBuff _buff in buffList)
            {
                if (buffDic[_buff] == Bufftype.Update)
                    _buff.Buff(mainModule);
            }
        }

        public void AddObserver(Observer _observer)
        {
            Observers.Add(_observer);
            _observer.Receive();
        }

        public void RemoveObserver(Observer _observer)
        {
            Observers.Remove(_observer);
            _observer.Receive();
        }

        public void Send()
        {
            foreach (Observer observer in Observers)
            {
                observer.Receive();
            }
        }
    }
}