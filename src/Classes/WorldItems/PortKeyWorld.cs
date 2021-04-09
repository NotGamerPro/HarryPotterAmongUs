﻿using System.Linq;
using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Classes.WorldItems
{
    public class PortKeyWorld : WorldItem
    {
        public static System.Random ItemRandom { get; set; } = new System.Random();
        public static float ItemSpawnChance { get; set; } = 20 * (Main.Instance.GetAllApplicableItemPositions().Count / 28);
        public static bool HasSpawned { get; set; }
        
        public PortKeyWorld(Vector2 position)
        {
            this.Position = position;
            this.Id = 2;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Port Key";
        }

        public static void WorldSpawn()
        {
            if (!CanSpawn())
                return;
            
            if (!ShipStatus.Instance)
                return;

            Vector2 pos = Main.Instance.GetAllApplicableItemPositions().Random();

            System.Console.WriteLine(pos.x + ":" + pos.y);

            if (Main.Instance.Config.SingleItem)
                Main.Instance.PossibleItemPositions.RemoveAll(x => x.Item2 == pos);

            Main.Instance.RpcSpawnItem(2, pos);
            HasSpawned = true;
        }
        
        public static bool CanSpawn()
        {
            if (Main.Instance.AllItems.Where(x => x.Id == 2).ToList().Count > 0)
                return false;

            if (MeetingHud.Instance)
                return false;

            if (!AmongUsClient.Instance.IsGameStarted)
                return false;

            if (ItemRandom.Next(0, 100000) > ItemSpawnChance)
                return false;
            
            if (HasSpawned && Main.Instance.Config.SingleItem)
                return false;

            return true;
        }
    }
}