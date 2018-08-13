#region References

using System;
using System.Collections;
using System.Linq;
using Server;
using Server.Commands;
using Server.Items;

#endregion

namespace Arya.Auction
{
    public class AuctionBotController : Item
    {
        private bool m_Enabled;
        private Mobile m_AuctionBot;
        private TimeSpan m_AuctionTimeSpan = TimeSpan.FromDays(1.0);
        private int m_NumberOfAuctions;
        private int m_MaxAuctions = 100;
        private int m_Randomizer;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                m_Enabled = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile AuctionBot
        {
            get { return m_AuctionBot; }
            set
            {
                m_AuctionBot = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan AuctionTimeSpan
        {
            get { return m_AuctionTimeSpan; }
            set
            {
                m_AuctionTimeSpan = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int NumberOfAuctions
        {
            get { return m_NumberOfAuctions; }
            set
            {
                m_NumberOfAuctions = value;

                if (m_NumberOfAuctions < 0)
                {
                    m_NumberOfAuctions = 0;
                }

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxAuctions
        {
            get { return m_MaxAuctions; }
            set
            {
                m_MaxAuctions = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Randomizer
        {
            get { return m_Randomizer; }
            set
            {
                m_Randomizer = value;
                InvalidateProperties();
            }
        }

        [Constructable]
        public AuctionBotController()
            : base(0xED4)
        {
            Movable = false;
            Hue = 0x489;
        }

        public AuctionBotController(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write(m_Enabled);
            writer.Write(m_AuctionBot);
            writer.Write(m_AuctionTimeSpan);
            writer.Write(m_NumberOfAuctions);
            writer.Write(m_MaxAuctions);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            reader.ReadInt();

            m_Enabled = reader.ReadBool();
            m_AuctionBot = reader.ReadMobile();
            m_AuctionTimeSpan = reader.ReadTimeSpan();
            m_NumberOfAuctions = reader.ReadInt();
            m_MaxAuctions = reader.ReadInt();
        }

        public static void RefreshAuctionBot()
        {
            Console.WriteLine("Auction Bot System Check.");

            ArrayList AuctionBotControllers = new ArrayList();

            foreach (AuctionBotController item in World.Items.Values.OfType<AuctionBotController>())
            {
                AuctionBotControllers.Add(item);
            }

            if (AuctionBotControllers.Count == 0)
            {
                Console.WriteLine("No Auction Bot Controllers Found.");
                return;
            }

            foreach (AuctionBotController controller in AuctionBotControllers.OfType<AuctionBotController>().Where(controller => controller.Enabled))
            {
                if (controller.AuctionBot == null)
                {
                    Console.WriteLine("Auction Bot is missing from an Auction Controller.");
                    continue;
                }

                //Auction Cleanup Needed - This helps us keep count of auctions that have ended and cleans out items from our auction bot.
                ArrayList AuctionBotCleanup = new ArrayList();
                BankBox botbank = controller.AuctionBot.BankBox;
                Backpack botpack = controller.AuctionBot.Backpack as Backpack;

                if (botbank != null)
                {
                    foreach (Item bankitem in botbank.Items)
                    {
                        AuctionBotCleanup.Add(bankitem);
                    }
                }

                if (botpack != null)
                {
                    foreach (Item packitem in botpack.Items)
                    {
                        AuctionBotCleanup.Add(packitem);
                    }
                }

                foreach (Item dirtyitem in AuctionBotCleanup)
                {
                    if (dirtyitem is AuctionItemCheck)
                    {
                        AuctionItemCheck ai = dirtyitem as AuctionItemCheck;

                        Item attached = ai.Item;

                        if (attached != null)
                        {
                            attached.Delete();
                        }
                    }

                    dirtyitem.Delete();
                    controller.NumberOfAuctions--;

                    if (controller.NumberOfAuctions < 0)
                    {
                        controller.NumberOfAuctions = 0;
                    }
                }
                //End Auction Cleanup

                bool refreshed = false;

                while ((controller.NumberOfAuctions + 1) <= controller.MaxAuctions)
                {
                    switch (Utility.Random(16))
                    {                 
                        case 0:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new Arrow(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 10 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} arrows.", amount),
                                Description = String.Format("{0} arrows.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 1:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new Bolt(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 12 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} bolts.", amount),
                                Description = String.Format("{0} bolts.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 2:
                        {                    
                            int amount = Utility.Random(10000) +1;
                            Item ai = new BlackPearl(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} black pearl.", amount),
                                Description = String.Format("{0} black pearl.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 3:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new BatWing(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} bat wing.", amount),
                                Description = String.Format("{0} bat wing.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 4:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new Bloodmoss(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} blood moss.", amount),
                                Description = String.Format("{0} blood moss.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 5:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new Garlic(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} garlic.", amount),
                                Description = String.Format("{0} garlic.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 6:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new Ginseng(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} ginseng.", amount),
                                Description = String.Format("{0} ginseng.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 7:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new MandrakeRoot(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} mandrake root.", amount),
                                Description = String.Format("{0} mandrake root.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 8:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new Nightshade(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} nightshade.", amount),
                                Description = String.Format("{0} nightshade.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 9:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new SpidersSilk(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} spiders silk.", amount),
                                Description = String.Format("{0} spiders silk.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 10:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new SulfurousAsh(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} sulfurous ash.", amount),
                                Description = String.Format("{0} sulfurous ash.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 11:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new DaemonBlood(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} daemon blood.", amount),
                                Description = String.Format("{0} daemon blood.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 12:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new PigIron(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} pig iron.", amount),
                                Description = String.Format("{0} pig iron.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 13:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new NoxCrystal(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} nox crystal.", amount),
                                Description = String.Format("{0} nox crystal.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 14:
                        {
                            int amount = Utility.Random(10000) +1;
                            Item ai = new GraveDust(amount);
                            ai.Amount = amount;

                            CommodityDeed com = new CommodityDeed();

                            if (com.SetCommodity(ai))
                            {
                                com.Hue = 0x592;
                            }
                            else
                            {
                                ai.Delete();
                                com.Delete();
                                goto case 0;
                            }

                            AuctionItem auction = new AuctionItem(com, controller.AuctionBot)
                            {
                                MinBid = 7 * amount,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                ItemName = String.Format("{0} grave dust.", amount),
                                Description = String.Format("{0} grave dust.", amount),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }
                        case 15:
                        {
                            if (Utility.RandomDouble() < 0.30)
                                goto case 0;

                            int randomamt = Utility.RandomList(3, 5, 7, 10, 12, 15, 20);
                            Item ai = new UncutCloth(randomamt);
                            ai.Hue = Utility.RandomList(1175, 1195, 1193, 1176, 1174, 1172, 1171, 1170, 1168, 1167, 1166, 1165, 1164, 1161, 1159, 1154, 1153, 1152, 1151, 1150, 1921, 1922, 1923, 1924, 1925, 1926, 1927, 1928, 1929, 1930, 1931, 1932, 1934, 1935, 1938, 1939, 1943, 1957, 1961, 1962, 1963, 1964, 1966, 1971, 1974, 1975, 2596, 2595, 2707, 2715, 2721, 2728, 2729, 2730, 2969, 2962, 2951, 2949);                      
                            AuctionItem auction = new AuctionItem(ai, controller.AuctionBot)
                            {
                                MinBid = 40000,
                                Reserve = 0,
                                Duration = controller.AuctionTimeSpan,
                                Description = String.Format("{0} pieces of special hued cloth.", randomamt),
                                WebLink = "",
                                BuyNow = 0
                            };

                            auction.Confirm();

                            controller.NumberOfAuctions++;
                            refreshed = true;

                            break;
                        }                       
                    }
                }
            }
        }
    }

    public class AuctionBotTimer : Timer
    {
        public static void Initialize()
        {
            new AuctionBotTimer();
            Console.WriteLine("Tresdni's Auction Bot Initialized!");
        }

        public AuctionBotTimer()
            : base(TimeSpan.FromSeconds(5), TimeSpan.FromHours(4))
        {
            Start();
        }

        protected override void OnTick()
        {
            AuctionBotController.RefreshAuctionBot();
        }
    }

    public class AuctionBotRefreshCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("AuctionBotRefresh", AccessLevel.Administrator, AuctionBotRefresh_OnCommand);
        }

        [Usage("AuctionBotRefresh")]
        [Description("Forces the system to do an auction bot check.")]
        public static void AuctionBotRefresh_OnCommand(CommandEventArgs e)
        {
            AuctionBotController.RefreshAuctionBot();
            e.Mobile.SendMessage(1153, "Auction Bot has been checked and refreshed if needed.");
        }
    }
}