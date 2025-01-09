using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TouhouPetsEx.Enhance.Core
{
	public class BaseEnhance
	{
        /// <summary>
        /// ����
        /// </summary>
        public virtual string Text => "";
        /// <summary>
        /// �Ƿ������Ҽ���Ĭ������
        /// </summary>
        public virtual bool EnableRightClick => true;
        /// <summary>
        /// ��ӱ���ǿ���Ӧ��Ʒ֮�����ϵ
        /// </summary>
        /// <param name="type">��Ӧ��Ʒ��type</param>
        public void AddEnhance(int type)
        {
            TouhouPetsEx.GEnhanceInstances[type] = this;
        }
        public virtual void ItemSSD()
        {

        }
        public virtual void ItemSD(Item item)
        {

        }
        public virtual void ItemHoldItem(Item item, Player player)
        {

        }
        public virtual void ItemUpdateInventory(Item item, Player player)
        {

        }
        public virtual void ItemModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

        }
        public virtual bool? ItemUseItem(Item item, Player player)
        {
            return null;
        }
        public virtual void PlayerPreUpdate(Player player)
        {

        }
        public virtual void PlayerPostUpdate(Player player)
        {

        }
        public virtual void PlayerPostUpdateEquips(Player player)
        {

        }
        public virtual void PlayerDrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {

        }
        public virtual void PlayerModifyHurt(Player player, ref Player.HurtModifiers modifiers)
        {

        }
        public virtual void PlayerModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {

        }
        public virtual void PlayerModifyLuck(Player player, ref float luck)
        {

        }
        public virtual void PlayerOnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        public virtual void NPCAI(NPC npc, Player player)
        {

        }
    }
}
