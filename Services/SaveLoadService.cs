// Services/SaveLoadService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AVGameRPG.Models;

namespace AVGameRPG.Services
{
    public static class SaveLoadService
    {
        private static readonly string SaveFile = Path.Combine(AppContext.BaseDirectory, "savegame.json");

        // MUSI być public, bo LoadGame(string) go zwraca
        public class SaveData
        {
            public Character Player { get; set; } = new Character();
            public List<Item> Inventory { get; set; } = new();
            public Equipment Equipment { get; set; } = new();
            public List<string> CompletedQuestIds { get; set; } = new();
        }

        // --- SINGLE FILE ---

        public static void SaveGame()
        {
            var data = new SaveData
            {
                Player = GameSession.Player,
                Inventory = GameSession.Inventory,
                Equipment = GameSession.Equipment,
                CompletedQuestIds = GameSession.CompletedQuestIds.ToList()
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SaveFile, json);
        }

        public static bool LoadGame()
        {
            if (!File.Exists(SaveFile)) return false;

            var json = File.ReadAllText(SaveFile);
            var data = JsonSerializer.Deserialize<SaveData>(json);
            if (data is null) return false;

            ApplyLoadedData(data);
            return true;
        }

        // --- MULTI FILES (lista/wybór) ---

        public static List<string> GetSaveFiles()
        {
            var dir = Path.Combine(AppContext.BaseDirectory, "saves");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return Directory.GetFiles(dir, "*.json").OrderByDescending(File.GetLastWriteTimeUtc).ToList();
        }

        public static void SaveGame(string path)
        {
            var data = new SaveData
            {
                Player = GameSession.Player,
                Inventory = GameSession.Inventory,
                Equipment = GameSession.Equipment,
                CompletedQuestIds = GameSession.CompletedQuestIds.ToList()
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        public static string SaveGameToSlot()
        {
            var dir = Path.Combine(AppContext.BaseDirectory, "saves");
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, $"save_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json");

            SaveGame(path); // używa tej, którą już masz (z parametrem path)

            return path;
        }


        public static SaveData? LoadGame(string path)
        {
            if (!File.Exists(path)) return null;
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<SaveData>(json);
        }

        // --- wspólne zastosowanie danych ---

        public static void ApplyLoadedData(SaveData data)
        {
            // Player
            GameSession.Player.CopyFrom(data.Player);

            // Inventory
            GameSession.Inventory.Clear();
            GameSession.Inventory.AddRange(data.Inventory);

            // Zdejmij aktualny sprzęt i załóż zapisany
            void UnequipIf(AVGameRPG.Models.Item? it)
            {
                if (it == null) return;
                GameSession.Equipment.Unequip(GameSession.Player, it.Category); // jeśli masz Unequip(slot)
            }
            void EquipIf(AVGameRPG.Models.Item? it)
            {
                if (it != null) GameSession.Equipment.Equip(GameSession.Player, it);
            }

            // zdejmij wszystko (jeśli brak metody zbiorczej – po slotach)
            UnequipIf(GameSession.Equipment.Head);
            UnequipIf(GameSession.Equipment.Armor);
            UnequipIf(GameSession.Equipment.Ring);
            UnequipIf(GameSession.Equipment.Necklace);
            UnequipIf(GameSession.Equipment.Gloves);
            UnequipIf(GameSession.Equipment.Weapon);
            UnequipIf(GameSession.Equipment.Shield);
            UnequipIf(GameSession.Equipment.Boots);
            UnequipIf(GameSession.Equipment.Misc);

            // nałóż zapisane
            EquipIf(data.Equipment.Head);
            EquipIf(data.Equipment.Armor);
            EquipIf(data.Equipment.Ring);
            EquipIf(data.Equipment.Necklace);
            EquipIf(data.Equipment.Gloves);
            EquipIf(data.Equipment.Weapon);
            EquipIf(data.Equipment.Shield);
            EquipIf(data.Equipment.Boots);
            EquipIf(data.Equipment.Misc);

            // Completed quests
            GameSession.CompletedQuestIds.Clear();
            foreach (var id in data.CompletedQuestIds)
                GameSession.CompletedQuestIds.Add(id);
        }

    }
}




