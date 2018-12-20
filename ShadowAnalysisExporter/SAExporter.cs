using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShadowAnalysisExporter
{
	public class SAExporter
	{
		private static void AddRibbonPanel(UIControlledApplication application)
		{
			RibbonPanel val = application.CreateRibbonPanel("Shadow Analysis 2 Exporter");
			string location = Assembly.GetExecutingAssembly().Location;
			PushButtonData val2 = new PushButtonData("cmdCurveTotalLength", "Export" + Environment.NewLine + "  model  ", location, "ShadowAnalysisExporter.MainExport");
			object obj = (object)(val.AddItem(val2) as PushButton);
			obj.set_ToolTip("Shadow Analysis 2 Exporter");
			BitmapImage largeImage = new BitmapImage(new Uri("pack://application:,,,/ShadowAnalysisExporter;component/Resources/shadow_logo.png"));
			obj.set_LargeImage((ImageSource)largeImage);
			ContextualHelp contextualHelp = new ContextualHelp(2, "http://deltacodes.pl/ShadowAnalysis2_Exporter_for_Revit-help");
			obj.SetContextualHelp(contextualHelp);
		}

		public Result OnShutdown(UIControlledApplication application)
		{
			return 0;
		}

		public Result OnStartup(UIControlledApplication application)
		{
			SAExporter.AddRibbonPanel(application);
			return 0;
		}
	}
}
