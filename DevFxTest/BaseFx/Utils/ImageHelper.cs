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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// ����ͼƬ�����һЩʵ�÷���
	/// </summary>
	public static class ImageHelper
	{
		/// <summary>
		/// ����ͼƬ������ͼ
		/// </summary>
		/// <param name="originalImage">ԭͼ</param>
		/// <param name="width">����ͼ�Ŀ����أ�</param>
		/// <param name="height">����ͼ�ĸߣ����أ�</param>
		/// <param name="mode">���Է�ʽ</param>
		/// <returns>����ͼ</returns>
		/// <remarks>
		///		<paramref name="mode"/>��
		///			<para>HW��ָ���ĸ߿����ţ����ܱ��Σ�</para>
		///			<para>HWO��ָ���߿����ţ����ܱ��Σ�����С�򲻱䣩</para>
		///			<para>W��ָ�����߰�����</para>
		///			<para>WO��ָ������С�򲻱䣩���߰�����</para>
		///			<para>H��ָ���ߣ�������</para>
		///			<para>HO��ָ���ߣ���С�򲻱䣩��������</para>
		///			<para>CUT��ָ���߿�ü��������Σ�</para>
		/// </remarks>
		public static Image MakeThumbnail(Image originalImage, int width, int height, string mode) {
			int towidth = width;
			int toheight = height;

			int x = 0;
			int y = 0;
			int ow = originalImage.Width;
			int oh = originalImage.Height;

			if(string.IsNullOrEmpty(mode)) {
				mode = "HW";
			} else {
				mode = mode.ToUpper();
			}

			switch(mode) {
				case "HW": //ָ���߿����ţ����ܱ��Σ�
					break;
				case "HWO": //ָ���߿����ţ����ܱ��Σ�����С�򲻱䣩
					if(originalImage.Width <= width && originalImage.Height <= height) {
						return originalImage;
					}
					if(originalImage.Width < width) {
						towidth = originalImage.Width;
					}
					if(originalImage.Height < height) {
						toheight = originalImage.Height;
					}
					break;
				case "W": //ָ�����߰�����
					toheight = originalImage.Height*width/originalImage.Width;
					break;
				case "WO": //ָ������С�򲻱䣩���߰�����
					if(originalImage.Width <= width) {
						return originalImage;
					} else {
						toheight = originalImage.Height*width/originalImage.Width;
					}
					break;
				case "H": //ָ���ߣ�������
					towidth = originalImage.Width*height/originalImage.Height;
					break;
				case "HO": //ָ���ߣ���С�򲻱䣩��������
					if(originalImage.Height <= height) {
						return originalImage;
					} else {
						towidth = originalImage.Width*height/originalImage.Height;
					}
					break;
				case "CUT": //ָ���߿�ü��������Σ�
					if((double)originalImage.Width/(double)originalImage.Height > (double)towidth/(double)toheight) {
						oh = originalImage.Height;
						ow = originalImage.Height*towidth/toheight;
						y = 0;
						x = (originalImage.Width - ow)/2;
					} else {
						ow = originalImage.Width;
						oh = originalImage.Width*height/towidth;
						x = 0;
						y = (originalImage.Height - oh)/2;
					}
					break;
				default:
					break;
			}

			//�½�һ��bmpͼƬ
			Image bitmap = new Bitmap(towidth, toheight);

			//�½�һ������
			Graphics g = Graphics.FromImage(bitmap);

			//���ø�������ֵ��
			g.InterpolationMode = InterpolationMode.High;

			//���ø�����,���ٶȳ���ƽ���̶�
			g.SmoothingMode = SmoothingMode.HighQuality;

			//��ջ�������͸������ɫ���
			g.Clear(Color.Transparent);

			//��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
			g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
			            new Rectangle(x, y, ow, oh),
			            GraphicsUnit.Pixel);
			g.Dispose();
			return bitmap;
		}

		/// <summary>
		/// ����ͼƬ������ͼ
		/// </summary>
		/// <param name="originalStream">ԭͼ</param>
		/// <param name="thumbnailPath">��������ͼ��·��</param>
		/// <param name="width">����ͼ�Ŀ����أ�</param>
		/// <param name="height">����ͼ�ĸߣ����أ�</param>
		/// <param name="mode">���Է�ʽ���μ�<seealso cref="MakeThumbnail(Image, int, int, string)"/></param>
		public static void MakeThumbnail(Stream originalStream, string thumbnailPath, int width, int height, string mode) {
			Image originalImage = Image.FromStream(originalStream);
			try {
				MakeThumbnail(originalImage, thumbnailPath, width, height, mode);
			} finally {
				originalImage.Dispose();
			}
		}

		/// <summary>
		/// ����ͼƬ������ͼ
		/// </summary>
		/// <param name="originalImage">ԭͼ</param>
		/// <param name="thumbnailPath">��������ͼ��·��</param>
		/// <param name="width">����ͼ�Ŀ����أ�</param>
		/// <param name="height">����ͼ�ĸߣ����أ�</param>
		/// <param name="mode">���Է�ʽ���μ�<seealso cref="MakeThumbnail(Image, int, int, string)"/></param>
		public static void MakeThumbnail(Image originalImage, string thumbnailPath, int width, int height, string mode) {
			Image bitmap = MakeThumbnail(originalImage, width, height, mode);
			try {
				//��jpg��ʽ��������ͼ
				bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
			} finally {
				bitmap.Dispose();
			}
		}

		/// <summary>
		/// ����ͼƬ������ͼ
		/// </summary>
		/// <param name="originalImagePath">ԭͼ��·��</param>
		/// <param name="thumbnailPath">��������ͼ��·��</param>
		/// <param name="width">����ͼ�Ŀ����أ�</param>
		/// <param name="height">����ͼ�ĸߣ����أ�</param>
		/// <param name="mode">���Է�ʽ���μ�<seealso cref="MakeThumbnail(Image, int, int, string)"/></param>
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode) {
			Image originalImage = Image.FromFile(originalImagePath);
			try {
				MakeThumbnail(originalImage, width, height, mode);
			} finally {
				originalImage.Dispose();
			}
		}
	}
}