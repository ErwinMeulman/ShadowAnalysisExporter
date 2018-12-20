using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShadowAnalysisExporter
{
	public class ReadyToExport : Form
	{
		private IContainer components;

		private Label label1;

		private Button button1;

		private Label label2;

		public ReadyToExport()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void label1_Click(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ReadyToExport));
			this.label1 = new Label();
			this.button1 = new Button();
			this.label2 = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 238);
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(432, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Your view is ready to export. Please click Revit icon in the top left corner,\r\n then select Export -> FBX.";
			this.label1.TextAlign = ContentAlignment.TopCenter;
			this.label1.Click += this.label1_Click;
			this.button1.DialogResult = DialogResult.OK;
			this.button1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 238);
			this.button1.Location = new Point(182, 108);
			this.button1.Name = "button1";
			this.button1.Size = new Size(98, 34);
			this.button1.TabIndex = 1;
			this.button1.Text = "I get it!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(75, 57);
			this.label2.Name = "label2";
			this.label2.Size = new Size(319, 26);
			this.label2.TabIndex = 2;
			this.label2.Text = "If you are using Revit 2017 and above, please make sure, \r\nthat selected file format (Files of type) is \"FBX 2015 and Previous\".";
			this.label2.TextAlign = ContentAlignment.MiddleCenter;
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(466, 154);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ReadyToExport";
			this.RightToLeftLayout = true;
			this.Text = "Your 3D view is ready!";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
