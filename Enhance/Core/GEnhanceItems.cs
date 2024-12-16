using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TouhouPetsEx.Enhance.Core
{
	public class GEnhanceItems : GlobalItem
    {
        private static void ProcessDemonismAction(Action<BaseEnhance> action)
        {
            foreach (var enhance in TouhouPetsEx.GEnhanceInstances)
            {
                action(enhance.Value);
            }
        }
        private static void ProcessDemonismAction(Item item, Action<BaseEnhance> action)
        {
            if (item.ModItem.Mod.Name == "TouhouPets" && TouhouPetsEx.GEnhanceInstances.TryGetValue(item.type, out var enhance))
            {
                action(enhance);
            }
        }
        public override void SetStaticDefaults()
        {
            List<Type> subclasses = new List<Type>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] allTypes = assembly.GetTypes();

            foreach (Type type in allTypes)
            {
                if (type.IsClass && !type.IsAbstract && typeof(BaseEnhance).IsAssignableFrom(type))
                {
                    subclasses.Add(type);
                }
            }

            foreach (Type types in subclasses)
            {
                object enhance = Activator.CreateInstance(types);
                BaseEnhance thisEnhance = enhance as BaseEnhance;
                thisEnhance.ItemSSD();
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem?.Mod.Name == "TouhouPets" && TouhouPetsEx.GEnhanceInstances.TryGetValue(item.type, out var enh))
            {
                // �ҵ�Ŀ�� Tooltip ������
                int targetIndex = tooltips
                .Select((t, index) => new { t.Name, Index = index }) // ��������
                .Where(x => x.Name.StartsWith("Tooltip") && int.TryParse(x.Name.Substring(7), out _)) // ɸѡ��Ч�ַ���
                .OrderByDescending(x => int.Parse(x.Name.Substring(7))) // �����ֽ�������
                .Select(x => x.Index) // ȡ����
                .FirstOrDefault(); // ��ȡ��һ�����

                tooltips.Insert(targetIndex + 1, new TooltipLine(Mod, "EnhanceTooltip", TouhouPetsExUtils.GetText("Common") + "\n" + enh.Text));
            }
        }
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.ModItem?.Mod.Name == "TouhouPets" && TouhouPetsEx.GEnhanceInstances.TryGetValue(item.type, out var enh))
                return enh.EnableRightClick;

            return false;
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (item.ModItem?.Mod.Name == "TouhouPets" && player.altFunctionUse == 2
                && TouhouPetsEx.GEnhanceInstances.TryGetValue(item.type, out var enh) && enh.EnableRightClick)
            {
                if (player.MP().ActiveEnhance.Contains(item.type))
                {
                    player.MP().ActiveEnhance.Remove(item.type);
                }
                else
                {
                    if (player.MP().ActiveEnhance.Count == player.MP().ActiveEnhanceCount)
                        player.MP().ActiveEnhance.RemoveAt(0);

                    player.MP().ActiveEnhance.Add(item.type);
                }

                return false;
            }

            return true;
        }
    }
}
