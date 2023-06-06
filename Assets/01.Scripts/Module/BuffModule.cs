using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buff;
using Pool;

namespace Module
{
    public class BuffModule : AbBaseModule,IObserble
    {
        public Dictionary<IBuff, BuffType> buffDic = new Dictionary<IBuff, BuffType>();
        public List<AbBuffEffect> buffList = new List<AbBuffEffect>();
        private List<Observer> observers = new List<Observer>();
            
        public List<Observer> Observers => observers;

        public BuffModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        
        public BuffModule() : base()
        {

        }


        public override void Start()
        {
            //AddBuff(new Healing_Buf(this).SetValue(10).SetDuration(10).SetPeriod(2).SetSpownObjectName("HealEffect").SetSprite("Demon"), BuffType.Update);
        }

        [ContextMenu("테스트")]
        public void TestBuff()
        {
            AddBuff(new Healing_Buf(this).SetValue(10).SetDuration(10).SetPeriod(2).SetSpownObjectName("HealEffect").SetSprite("Demon"), BuffType.Update);

        }
        public void AddBuff(AbBuffEffect _buff, BuffType _bufftype)
        {
            buffDic.Add(_buff, _bufftype);
            buffList.Add(_buff);

            if (_bufftype == BuffType.Once)
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
                if (buffDic[_buff] == BuffType.Update)
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
        
        public override void OnDisable()
        {
            buffDic.Clear();
            buffList.Clear();
            observers.Clear();
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<BuffModule>(this);
        }
        
        public override void OnDestroy()
        {
            buffDic.Clear();
            buffList.Clear();
            observers.Clear();
            base.OnDestroy();
            ClassPoolManager.Instance.RegisterObject<BuffModule>(this);
        }
    }
}