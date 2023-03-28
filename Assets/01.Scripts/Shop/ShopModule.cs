using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Shop;
using Utill.Addressable;
using Utill.Pattern;

namespace Module.Shop
{
	public class ShopModule : AbBaseModule
	{
		public ShopSO ShopSO
		{
			get
			{
				return shopSO;
			}
			set
			{
				shopSO = value;
			}
		}
		private ShopSO shopSO;

		public ShopModule(AbMainModule _mainModule, string _shopAddress) : base(_mainModule)
		{
		}

		public ShopModule() : base()
		{
			
		}

		public override void Init(AbMainModule _mainModule, params string[] _parameters)
		{
			base.Init(_mainModule, _parameters);
			shopSO = AddressablesManager.Instance.GetResource<ShopSO>(_parameters[0]);
		}

		public void Register()
		{
			ShopManager.Instance.SetShopModule(this);
		}
	}
}
