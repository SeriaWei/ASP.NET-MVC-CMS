using Easy.Data;
using Easy.Modules.DataDictionary;
using Easy.Web.CMS.PackageManger;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace Easy.Web.CMS.Widget
{
    public class WidgetPackageInstaller : FilePackageInstaller
    {
        public override string PackageInstaller
        {
            get
            {
                return "WidgetPackageInstaller";
            }
        }
        public override object Install(Package package)
        {
            base.Install(package);
            var widgetPackage = package as WidgetPackage;
            if (widgetPackage != null)
            {
                if (widgetPackage.Widget != null)
                {
                    var widget = JsonConvert.DeserializeObject(JObject.Parse(package.Content.ToString()).GetValue("Widget").ToString(), widgetPackage.Widget.GetViewModelType()) as WidgetBase;
                    widget.PageID = null;
                    widget.LayoutID = null;
                    widget.ZoneID = null;
                    widget.IsSystem = false;
                    widget.IsTemplate = true;
                    return widget;
                }
            }
            return null;
        }
        public override Package Pack(object obj)
        {
            //obj is widget
            var package = base.Pack(GetWidgetFiles());
            (package as WidgetPackage).Widget = obj as WidgetBase;
            return package;
        }
        public override FilePackage CreatePackage()
        {
            return new WidgetPackage(PackageInstaller);
        }
        public virtual IEnumerable<System.IO.FileInfo> GetWidgetFiles()
        {
            return null;
        }
    }
    public class DataDictionaryPackageInstaller : FilePackageInstaller
    {
        public override string PackageInstaller
        {
            get
            {
                return "DataDictionaryPackageInstaller";
            }
        }
        public override object Install(Package package)
        {
            base.Install(package);
            DataDictionaryPackage dicPackage = package as DataDictionaryPackage;
            if (dicPackage != null)
            {
                var dataDictionaryService = ServiceLocator.Current.GetInstance<IDataDictionaryService>();
                var exists = dataDictionaryService.Count(new DataFilter().Where("DicName", OperatorType.Equal, dicPackage.DataDictionary.DicName).Where("DicValue", OperatorType.Equal, dicPackage.DataDictionary.DicValue));
                if (exists == 0)
                {
                    dataDictionaryService.Add(dicPackage.DataDictionary);
                }
            }

            return null;
        }
        public override Package Pack(object obj)
        {
            DataDictionaryPackage package = null;
            if (OnPacking != null)
            {
                package = base.Pack(OnPacking()) as DataDictionaryPackage;
            }
            if (package == null)
            {
                package = CreatePackage() as DataDictionaryPackage;
            }
            package.DataDictionary = obj as DataDictionaryEntity;
            return package;
        }
        public override FilePackage CreatePackage()
        {
            return new DataDictionaryPackage(PackageInstaller);
        }
        public Func<IEnumerable<System.IO.FileInfo>> OnPacking { get; set; }
    }
}
