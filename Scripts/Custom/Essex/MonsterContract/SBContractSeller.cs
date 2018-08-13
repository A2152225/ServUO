using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBContractSeller : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBContractSeller()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( MonsterContract ), 100000, 20, 0x14EF, 18 ) ); 
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( MonsterContract ), 1000 );
			}
		}
	}
}
