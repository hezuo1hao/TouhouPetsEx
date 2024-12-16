using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using TouhouPetsEx.Enhance.Core;
using TouhouPets.Content.Items.PetItems;
using Terraria.ID;

namespace TouhouPetsEx
{
    public class TouhouPetsExModCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "give";

        public override string Usage => TouhouPetsExUtils.GetText("Give.Usage");

        public override string Description => TouhouPetsExUtils.GetText("Give.Description");

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            // ��������������ָ�����
            if (!caller.Player.MP().ActiveEnhance.Contains(ModContent.ItemType<ShinkiHeart>()))
                throw new UsageException(TouhouPetsExUtils.GetText("Give.Error_1"));

            // һ��������û�У�����
            if (args.Length == 0)
                throw new UsageException(TouhouPetsExUtils.GetText("Give.Error_2"));

            // ���Ի�ȡָ����������û�����֣�û����ζ����д�������ƻ���Ϲ�������
            if (!int.TryParse(args[0], out int type))
            {
                // ����д�������е��»����滻�ɿո�
                string name = args[0].Replace("_", " ");

                // ����������Ʒ����ѯ�Ƿ��з��ϵ�ǰ���Ƶ���Ʒ�������ִ�Сд��
                for (int k = 1; k < ItemLoader.ItemCount; k++)
                {
                    if (name.ToLower() == Lang.GetItemNameValue(k).ToLower())
                    {
                        type = k;
                        break;
                    }
                }
            }

            // �Ҳ�����Ӧ����Ʒ������
            if (type <= 0 || type >= ItemLoader.ItemCount)
                throw new UsageException(TouhouPetsExUtils.GetText("Give.Error_3", ItemLoader.ItemCount));

            // ��ͼ����ֶ���������
            Item item = new(type);
            if (item.rare != ItemRarityID.White || item.value > 0 || item.createTile == -1 || !Main.tileSolid[item.createTile] || item.type is 424 or 1103 or 3347)
                throw new UsageException(TouhouPetsExUtils.GetText("Give.Error_4"));

            // ���������Ʒ�����ͻ�ȡ��û����Ĭ��Ϊ1
            int stack = 1;
            if (args.Length >= 2)
            {
                // ����Ϲ���������
                if (!int.TryParse(args[1], out stack))
                    throw new UsageException(TouhouPetsExUtils.GetText("Give.Error_5") + args[1]);
            }

            // ������������������������Ʒ
            caller.Player.QuickSpawnItem(new EntitySource_DebugCommand($"{nameof(TouhouPetsEx)}_{nameof(TouhouPetsExModCommand)}"), type, stack);
        }
    }
}
