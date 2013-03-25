/******************************************************************************
	Copyright 2005-2007 R2@DevFx.NET 
	DevFx.NET is free software; you can redistribute it and/or modify
	it under the terms of the Lesser GNU General Public License as published by
	the Free Software Foundation; either version 2 of the License, or
	(at your option) any later version.

	DevFx.NET is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	Lesser GNU General Public License for more details.

	You should have received a copy of the Lesser GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
/*******************************************************************************/

using HTB.DevFx.Config;

namespace HTB.DevFx.Core
{
	/// <summary>
	/// ���ģ��ӿ�
	/// </summary>
	/// <remarks>
	/// ģ����ҪǶ�뵽����У��밴���¸�ʽ���ã�
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;module name="ģ������" type="ʵ��ģ��ӿڵ�����" configName="��ģ��ʵ�ʵ����ý���" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	///	IFrameworkʵ����������õ�ģ����г�ʼ��������<c>IModule.Init(IFramework, IConfigSetting)</c>
	/// </remarks>
	public interface IModule
	{
		/// <summary>
		/// ģ����
		/// </summary>
		string Name { get; }

		/// <summary>
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		void Init(IFramework framework, IConfigSetting setting);

		/// <summary>
		/// ��ģ������ý�
		/// </summary>
		IConfigSetting Setting { get; }

		/// <summary>
		/// ��ȡ��ģ���ʵ���������ǵ���ģʽҲ�����Ƕ���ģʽ��
		/// </summary>
		/// <returns>IModule</returns>
		IModule GetInstance();
	}
}
