using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TouhouPets.Content.Items.PetItems;
using TouhouPetsEx.Enhance.Core;

namespace TouhouPetsEx.Enhance.Achieve
{
    public class HecatiaAndPiece : BaseEnhance
    {
        public override string Text => TouhouPetsExUtils.GetText("HecatiaAndPiece");
        public override bool EnableRightClick => false;
        public override void ItemSSD()
        {
            AddEnhance(ModContent.ItemType<HecatiaPlanet>());
        }
        public override void ItemUpdateInventory(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<HecatiaPlanet>() && !player.MP().ActiveEnhance.Contains(ModContent.ItemType<HecatiaPlanet>()))
            {
                player.MP().ActiveEnhanceCount++;
                player.MP().ActiveEnhance.Add(ModContent.ItemType<HecatiaPlanet>());
            }
        }
        public override void PlayerPreUpdate(Player player)
        {
            if (Main.hardMode)
                player.MP().ActiveEnhanceCount++;

            if (NPC.downedMoonlord)
                player.MP().ActiveEnhanceCount++;
        }
        public override void NPCAI(NPC npc, Player player)
        {
            int cycle = 60; // ����
            int Radius = 15;
            int minTileX = (int)(npc.position.X / 16f - Radius);
            int maxTileX = (int)(npc.position.X / 16f + Radius);
            int minTileY = (int)(npc.position.Y / 16f - Radius);
            int maxTileY = (int)(npc.position.Y / 16f + Radius);

            // ȷ�����귶Χ����Ч��Χ��
            if (minTileX < 0)
            {
                minTileX = 0;
            }
            if (maxTileX > Main.maxTilesX)
            {
                maxTileX = Main.maxTilesX;
            }
            if (minTileY < 0)
            {
                minTileY = 0;
            }
            if (maxTileY > Main.maxTilesY)
            {
                maxTileY = Main.maxTilesY;
            }

            // ��ǰ�����еĲ���
            int totalTiles = (maxTileX - minTileX + 1) * (maxTileY - minTileY + 1);
            int stepCount = totalTiles / cycle;  // ÿ�θ���ʱ�������ש��

            if (stepCount == 0) stepCount = 1;  // ��ֹ����0��ȷ�����ٴ���һ��ש��

            int stepIndex = (int)(Main.time % cycle); // ��ǰʱ�����������ڲ���

            // ���㵱ǰ����Ҫ�����ש��
            int startTile = minTileX + (stepIndex * stepCount); // ���ĸ�ש�鿪ʼ
            int endTile = startTile + stepCount - 1;  // ���ĸ�ש�����

            // ���ƽ���λ�ò�������Χ
            if (endTile > maxTileX) endTile = maxTileX;

            // ��ִ��ÿ��ש��Ĳ���
            for (int i = startTile; i <= endTile; i++)
            {
                for (int j = minTileY; j <= maxTileY; j++)
                {
                    float diffX = Math.Abs(i - npc.position.X / 16f);
                    float diffY = Math.Abs(j - npc.position.Y / 16f);
                    double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));

                    // �������С���趨�İ뾶�����Ҹ�λ���ǻ�����͵�ש��
                    if (distanceToTile < Radius && TileID.Sets.Torch[Framing.GetTileSafely(i, j).TileType])
                    {
                        npc.SimpleStrikeNPC(1, 0); // �� NPC ����˺�
                    }

                    // ���ɻ��ķ۳�Ч��
                    Dust.NewDustDirect(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 1, 1, DustID.Torch).noGravity = true;
                }
            }
        }
    }
}
