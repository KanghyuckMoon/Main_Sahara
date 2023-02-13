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
			shopSO = AddressablesManager.Instance.GetResource<ShopSO>(_shopAddress);
		}

		public void Register()
		{
			ShopManager.Instance.SetShopModule(this);
		}
	}
}
