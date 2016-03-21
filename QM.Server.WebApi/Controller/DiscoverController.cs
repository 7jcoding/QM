using QM.Common;
using QM.Server.WebApi.Entity;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QM.Server.WebApi.Controller {

    public class DiscoverController : ApiController {

        private static readonly string Root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="traversal">是否遍历子目录</param>
        /// <param name="showFolder">是否返回文件夹</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DiscoverResult> List(string relativePath = null, bool traversal = false, bool showFolder = true) {
            // Path.Combin(@"d:\AAA", @"c:\") will return c:\
            var path = Path.Combine(Root, relativePath ?? "");
            if (path.StartsWith(Root, StringComparison.OrdinalIgnoreCase)) {
                if (Directory.Exists(path)) {
                    var dlls = this.Filter(Directory.GetFiles(path, "*.dll", traversal ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                        .Select(d => new DiscoverResult() {
                            IsFile = true,
                            RelativePath = d.Replace(Root, "")
                        });
                    var dirs = Directory.GetDirectories(path)
                        .Select(d => new DiscoverResult() {
                            IsFile = false,
                            RelativePath = d.Replace(Root, "")
                        });
                    //var t = this.T(path);
                    //return dirs.Concat(t);
                    if (showFolder)
                        return dlls.Concat(dirs);
                    else
                        return dlls;
                }
            }
            return Enumerable.Empty<DiscoverResult>();
        }

        [HttpGet]
        public IEnumerable<JobTypeInfo> GetTypes(string dllPath) {
            //var path = Path.Combine(Root, dllPath);
            var path = string.Format(@"{0}\{1}", Root, dllPath);
            if (path.StartsWith(Root) && File.Exists(path)) {
                var asm = Assembly.LoadFrom(path);
                var types = asm.GetTypes()
                    .Where(t => typeof(IJob).IsAssignableFrom(t)
                            && t.IsPublic
                            && !t.IsAbstract);

                foreach (var t in types) {
                    var desc = t.GetCustomAttribute<DescriptionAttribute>();
                    //TODO
                    yield return new JobTypeInfo() {
                        Desc = desc != null ? desc.Description : "",
                        FullName = t.FullName,
                        Name = t.Name,
                        Params = this.GetJobParametersFromJobType(t),
                        AssemblyQualifiedName = t.AssemblyQualifiedName,
                        AssemblyFile = t.Assembly.Location
                    };
                }
            }
        }

        private IEnumerable<string> Filter(IEnumerable<string> files) {
            foreach (var f in files) {
                var flag = false;
                try {
                    var asm = Assembly.LoadFrom(f);
                    flag = asm.GetTypes()
                                .Any(t => typeof(IJob).IsAssignableFrom(t)
                                && t.IsPublic
                                && !t.IsAbstract);

                } catch {

                }

                if (flag)
                    yield return f;
            }
        }

        private IEnumerable<JobParameterInfo> GetJobParametersFromJobType(Type jobType) {
            var attr = jobType.GetCustomAttribute<ParameterTypeAttribute>(false);
            if (attr != null) {
                var dic = DatamapParser.GetSupportProperties(attr.ParameterType);
                return dic.Select(d => {
                    var desc = d.Key.GetCustomAttribute<DescriptionAttribute>();
                    return new JobParameterInfo() {
                        Name = d.Key.Name,
                        Value = d.Value != null ? d.Value.ToString() : null,
                        Desc = desc != null ? desc.Description : d.Key.Name,
                        Type = d.Key.PropertyType.Name
                    };
                });
            }

            return new List<JobParameterInfo>();
        }

        //private IEnumerable<string> T(string dir) {
        //    var dlls = Directory.GetFiles(dir, "*.dll");
        //    if (dlls.Count() == 0)
        //        yield return null;

        //    AppDomainSetup setup = new AppDomainSetup();
        //    setup.ApplicationName = "tmp";
        //    setup.ApplicationBase = dir;
        //    setup.PrivateBinPath = dir;
        //    setup.DisallowApplicationBaseProbing = true;

        //    var domain = AppDomain.CreateDomain("tmp", null, setup);
        //    AssemblyResolver.Register(domain);
        //    foreach (var d in dlls) {
        //        var name = Assembly.LoadFrom(d).GetName();
        //        domain.Load(name);
        //    }

        //    var asms = domain.GetAssemblies();
        //    foreach (var asm in asms) {
        //        var flag = false;
        //        try {
        //            flag = asm.GetTypes()
        //                        .Any(t => typeof(IJob).IsAssignableFrom(t)
        //                        && t.IsPublic
        //                        && !t.IsAbstract);
        //        } catch (Exception e) {

        //        }
        //        if (flag)
        //            yield return asm.FullName;

        //    }
        //}
    }
}
