﻿/* Copyright(c) 2016 UltimaLive
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using UltimaLive;
using Server.Engines.Harvest;


namespace UltimaLive.LumberHarvest
{
  public class GraphicAsset
  {
    public int ItemID;
    public Int16 XOffset;
    public Int16 YOffset;
    public int HarvestResourceBaseAmount = 0;
    public int BonusResourceBaseAmount = 0;

    //Graphics are linked to bonus resources only. Regular harvest resources should be assigned at the phase level.
    public Dictionary<int, BonusHarvestResource[]> BonusResources = new Dictionary<int, BonusHarvestResource[]>();

    public GraphicAsset(int itemId, Int16 xOff, Int16 yOff)
    {
      ItemID = itemId;
      XOffset = xOff;
      YOffset = yOff;
    }

    public virtual List<Item> ReapBonusResources(int hue, Mobile from)
    {
      return ReapBonusResources(hue, from, BonusResourceBaseAmount, BonusResources);
    }

    //The bonus resource list can be specified at the phase level by using this overloaded method.
    public static List<Item> ReapBonusResources(int hue, Mobile from, int bonusResourceAmount, Dictionary<int, BonusHarvestResource[]> bonusList)
    {
      List<Item> bonusItems = new List<Item>();
      BonusHarvestResource[] bonusResourceList = null;

      if (bonusResourceAmount > 0)
      {
        if (bonusList.ContainsKey(hue))
        {
          bonusResourceList = bonusList[hue];
        }
        else if (bonusList.ContainsKey(0))
        {
          bonusResourceList = bonusList[0];
        }

        if (bonusResourceList != null && bonusResourceList.Length > 0)
        {
          double skillBase = from.Skills[SkillName.Lumberjacking].Base;

          foreach (BonusHarvestResource resource in bonusResourceList)
          {
            if (skillBase >= resource.ReqSkill && Utility.RandomDouble() <= resource.Chance)
            {
              try
              {
                Item item = Activator.CreateInstance(resource.Type) as Item;
                if (item != null)
                {
                  item.Amount = bonusResourceAmount;
                  bonusItems.Add(item);
                }
              }
              catch
              {
                Console.WriteLine("exception caught when trying to create bonus resource");
              }
            }
          }
        }
      }

      return bonusItems;
    }
  }
}
