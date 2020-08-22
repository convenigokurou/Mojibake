using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Mojibake
{
	public partial class Form1 : Form
	{
		private bool initEnd = false;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			List<ComboEncode> encList = new List<ComboEncode>();

			//utf8 65001 
			//sjis 932

			foreach( EncodingInfo ei in Encoding.GetEncodings() )
			{
				encList.Add( new ComboEncode(ei) );
			}

			cbIn.Items.AddRange(encList.ToArray());
			cbOut.Items.AddRange(encList.ToArray());

			for( int i = 0; i < encList.Count; i++ )
			{
				if( encList[i].CodePage == 932 )
				{
					cbIn.SelectedIndex = i;
				}
				else if( encList[i].CodePage == 65001 )
				{
					cbOut.SelectedIndex = i;
				}
			}
			
			initEnd = true;
			OutputText();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			OutputText();
		}

		private void cbIn_SelectedIndexChanged(object sender, EventArgs e)
		{
			OutputText();
		}

		private void cbOut_SelectedIndexChanged(object sender, EventArgs e)
		{
			OutputText();
		}

		private void OutputText()
		{
			if( !initEnd ) return;

			string src = textBox1.Text;
			Encoding utf8 = ((ComboEncode)cbOut.SelectedItem).Encoding;
			Encoding sjis = ((ComboEncode)cbIn.SelectedItem).Encoding;
			byte[] utf8byte = sjis.GetBytes(src);
			textBox2.Text = utf8.GetString(utf8byte);
		}

	}

	public class ComboEncode
	{
		public int CodePage 
		{
			get
			{
				return encInfo.CodePage;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return encInfo.GetEncoding();
			}
		}

		private EncodingInfo encInfo;

		public ComboEncode( EncodingInfo ei )
		{
			encInfo = ei;
		}

		public override string ToString()
		{
			return $"{encInfo.DisplayName}[{encInfo.CodePage}]";
		}
	}
}
