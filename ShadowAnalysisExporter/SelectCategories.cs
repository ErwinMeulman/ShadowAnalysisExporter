using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShadowAnalysisExporter
{
	public class SelectCategories : Form
	{
		public static class EnumParseUtility<TEnum>
		{
			public static TEnum Parse(string strValue)
			{
				if (!typeof(TEnum).IsEnum)
				{
					return default(TEnum);
				}
				return (TEnum)Enum.Parse(typeof(TEnum), strValue);
			}

			public static string Parse(TEnum enumVal)
			{
				if (!typeof(TEnum).IsEnum)
				{
					return string.Empty;
				}
				return Enum.GetName(typeof(TEnum), enumVal);
			}

			public static string Parse(int enumValInt)
			{
				if (!typeof(TEnum).IsEnum)
				{
					return string.Empty;
				}
				return Enum.GetName(typeof(TEnum), enumValInt);
			}
		}

		private List<Category> defaultCategories;

		private Document doc;

		private View3D view;

		private IContainer components;

		private Button button1;

		private Button button2;

		private CheckedListBox checkedListBox1;

		private Label label1;

		private CheckedListBox checkedListBox2;

		private Label label2;

		private Label label3;

		public SelectCategories()
		{
			this.InitializeComponent();
		}

		public SelectCategories(Document doc, View3D view, List<Category> defaultCategories)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			this.InitializeComponent();
			List<ElementId> list = new List<ElementId>();
			this.defaultCategories = defaultCategories;
			this.view = view;
			this.doc = doc;
			foreach (Category defaultCategory in defaultCategories)
			{
				this.checkedListBox1.Items.Add(defaultCategory.get_Name(), true);
				list.Add(defaultCategory.get_Id());
			}
			this.AddAppliableCategories(list);
		}

		private void AddAppliableCategories(List<ElementId> defaultCategories)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			this.checkedListBox2.Items.Clear();
			foreach (ElementId allFilterableCategory in ParameterFilterUtilities.GetAllFilterableCategories())
			{
				try
				{
					Category category = Category.GetCategory(this.doc, allFilterableCategory);
					if (!defaultCategories.Contains(allFilterableCategory) && category.get_AllowsVisibilityControl(this.view))
					{
						category.set_Visible(this.view, false);
						this.checkedListBox2.Items.Add(EnumParseUtility<BuiltInCategory>.Parse(allFilterableCategory.get_IntegerValue()));
					}
				}
				catch
				{
					Console.WriteLine("Coś poszło nie tak");
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			foreach (ElementId allFilterableCategory in ParameterFilterUtilities.GetAllFilterableCategories())
			{
				try
				{
					Category category = Category.GetCategory(this.doc, allFilterableCategory);
					if (category.get_AllowsVisibilityControl(this.view))
					{
						category.set_Visible(this.view, false);
					}
				}
				catch
				{
					Console.WriteLine("Coś poszło nie tak");
				}
			}
			foreach (object checkedItem in this.checkedListBox1.CheckedItems)
			{
				foreach (Category defaultCategory in this.defaultCategories)
				{
					if (checkedItem.ToString() == defaultCategory.get_Name())
					{
						defaultCategory.set_Visible(this.view, true);
					}
				}
			}
			foreach (object checkedItem2 in this.checkedListBox2.CheckedItems)
			{
				BuiltInCategory val = EnumParseUtility<BuiltInCategory>.Parse(checkedItem2.ToString());
				try
				{
					Category.GetCategory(this.doc, val).set_Visible(this.view, true);
				}
				catch
				{
					Console.WriteLine("Coś poszło nie tak");
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SelectCategories));
			this.button1 = new Button();
			this.button2 = new Button();
			this.checkedListBox1 = new CheckedListBox();
			this.label1 = new Label();
			this.checkedListBox2 = new CheckedListBox();
			this.label2 = new Label();
			this.label3 = new Label();
			base.SuspendLayout();
			this.button1.DialogResult = DialogResult.OK;
			this.button1.FlatStyle = FlatStyle.System;
			this.button1.Location = new Point(345, 338);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Export";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			this.button2.DialogResult = DialogResult.Cancel;
			this.button2.Location = new Point(264, 338);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += this.button2_Click;
			this.checkedListBox1.AccessibleName = "Default Categories";
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new Point(12, 73);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new Size(200, 259);
			this.checkedListBox1.Sorted = true;
			this.checkedListBox1.TabIndex = 2;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 238);
			this.label1.Location = new Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(411, 26);
			this.label1.TabIndex = 3;
			this.label1.Text = "Select categories that you want to include in exported model.\r\nKeep in mind that the more geometry you export, the more time it will take to analyze it.";
			this.checkedListBox2.FormattingEnabled = true;
			this.checkedListBox2.Location = new Point(220, 73);
			this.checkedListBox2.Name = "checkedListBox2";
			this.checkedListBox2.Size = new Size(200, 259);
			this.checkedListBox2.TabIndex = 4;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 238);
			this.label2.Location = new Point(9, 47);
			this.label2.Name = "label2";
			this.label2.Size = new Size(115, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Default categories:";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(217, 47);
			this.label3.Name = "label3";
			this.label3.Size = new Size(88, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Other categories:";
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.button2;
			base.ClientSize = new Size(435, 371);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.checkedListBox2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.checkedListBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.ImeMode = ImeMode.Disable;
			base.KeyPreview = true;
			base.Name = "Select";
			this.Text = "Shadow Analysis Exporter";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
