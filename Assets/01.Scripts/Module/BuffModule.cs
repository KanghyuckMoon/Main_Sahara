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

    public class BuffModule : AbBaseModule
    {
        public Dictionary<IBuff, Bufftype> buffDic = new Dictionary<IBuff, Bufftype>();

        public BuffModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            AddBuff(new Healing_Buf(this).SetValue(10).SetDuration(10).SetPeriod(2).SetSpownObjectName("HealEffect"), Bufftype.Update);
        }

        public void AddBuff(IBuff _buff, Bufftype _bufftype)
        {
            buffDic.Add(_buff, _bufftype);
            if (_bufftype == Bufftype.Once)
            {
                _buff.Buff(mainModule);
                buffDic.Remove(_buff);
            }
        }

        public override void Update()
        {
            foreach(IBuff _buff in buffDic.Keys)
            {
                if (buffDic[_buff] == Bufftype.Update)
                    _buff.Buff(mainModule);
            }
        }
    }
}