using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShadowAnalysisExporter
{
	[Transaction(TransactionMode.Manual)]
	public class MainExport
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			UIApplication application = commandData.get_Application();
			UIDocument activeUIDocument = application.get_ActiveUIDocument();
			application.get_Application();
			Document document = activeUIDocument.get_Document();
			View3D val = this.Get3dView(document);
			if ((int)val == 0)
			{
				message = "Sorry, no suitable 3D view found";
				return -1;
			}
			activeUIDocument.set_ActiveView(val);
			Transaction val2 = new Transaction(document);
			val2.Start("Change to 3D view");
			val.get_Parameter(-1011002).Set(3);
			val.get_Parameter(-1005165).Set(2);
			List<Category> list = new List<Category>();
			list.Add(Category.GetCategory(document, -2000038));
			list.Add(Category.GetCategory(document, -2000100));
			list.Add(Category.GetCategory(document, -2000171));
			list.Add(Category.GetCategory(document, -2000170));
			list.Add(Category.GetCategory(document, -2000023));
			list.Add(Category.GetCategory(document, -2000032));
			list.Add(Category.GetCategory(document, -2001180));
			list.Add(Category.GetCategory(document, -2000180));
			list.Add(Category.GetCategory(document, -2001220));
			list.Add(Category.GetCategory(document, -2000035));
			list.Add(Category.GetCategory(document, -2001260));
			list.Add(Category.GetCategory(document, -2000120));
			list.Add(Category.GetCategory(document, -2001340));
			list.Add(Category.GetCategory(document, -2000011));
			list.Add(Category.GetCategory(document, -2000014));
			if (new SelectCategories(document, val, list).ShowDialog() == DialogResult.Cancel)
			{
				return 1;
			}
			val2.Commit();
			new ReadyToExport().ShowDialog();
			return 0;
		}

		public View3D Get3dView(Document doc)
		{
			FilteredElementCollector val = new FilteredElementCollector(doc).OfClass(typeof(View3D));
			Regex regex = new Regex("^Shadow Analysis 2 Export [0-9]*$");
			Regex regex2 = new Regex("[0-9]*$");
			int num = 0;
			foreach (Element item in val)
			{
				View3D val2 = item;
				Match match = regex.Match(val2.get_Name());
				if (match.Success)
				{
					int num2 = int.Parse(regex2.Match(match.Value).Value);
					if (num < num2)
					{
						num = num2;
					}
				}
			}
			Transaction val3 = new Transaction(doc);
			val3.Start("Create 3D view");
			ViewFamilyType val4 = ((IEnumerable)new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))).Cast<ViewFamilyType>().FirstOrDefault<ViewFamilyType>((Func<ViewFamilyType, bool>)((ViewFamilyType x) => 102 == (int)x.get_ViewFamily()));
			View3D val5 = View3D.CreateIsometric(doc, val4.get_Id());
			val5.set_Name("Shadow Analysis 2 Export " + (num + 1).ToString());
			val3.Commit();
			return val5;
		}
	}
}
