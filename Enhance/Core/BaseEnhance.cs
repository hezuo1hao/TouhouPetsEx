using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
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
        public virtual void PlayerPostUpdate(Player player)
        {

        }
    }
}
