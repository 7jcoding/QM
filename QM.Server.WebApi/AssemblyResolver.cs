using System;
using System.Reflection;

namespace QM.Server.WebApi {
    internal class AssemblyResolver : MarshalByRefObject {
        static internal void Register(AppDomain domain) {
            var resolver =
                domain.CreateInstanceFromAndUnwrap(
                  Assembly.GetExecutingAssembly().Location,
                  typeof(AssemblyResolver).FullName) as AssemblyResolver;

            resolver.RegisterDomain(domain);
        }

        private void RegisterDomain(AppDomain domain) {
            domain.AssemblyResolve += ResolveAssembly;
            domain.AssemblyLoad += LoadAssembly;
            domain.ReflectionOnlyAssemblyResolve += Domain_ReflectionOnlyAssemblyResolve;
            domain.TypeResolve += Domain_TypeResolve;
        }

        private Assembly Domain_TypeResolve(object sender, ResolveEventArgs args) {
            return null;
        }

        private Assembly Domain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args) {
            return null;
        }

        private Assembly ResolveAssembly(object sender, ResolveEventArgs args) {
            // implement assembly resolving here
            return null;
        }

        private void LoadAssembly(object sender, AssemblyLoadEventArgs args) {
            // implement assembly loading here
        }
    }
}
