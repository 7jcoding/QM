using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM.RemoteLoader {
    public class RemoteLoader : MarshalByRefObject {
        public Assembly CurrAssembly {
            get;
            private set;
        }

        internal void LoadAssembly(string assemblyFile) {
            this.CurrAssembly = Assembly.LoadFrom(assemblyFile);
        }

        public object GetInstance(string typeName) {
            if (this.CurrAssembly == null)
                return null;
            var type = this.CurrAssembly.GetType(typeName);
            if (type == null)
                return null;
            return Activator.CreateInstance(type);
        }

        public void ExecuteMothod(string typeName, string methodName) {
            if (this.CurrAssembly == null)
                return;
            var type = this.CurrAssembly.GetType(typeName);
            var obj = Activator.CreateInstance(type);
            Expression<Action> lambda = Expression.Lambda<Action>(Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
            lambda.Compile()();
        }

        public T ExecuteMethod<T>(string typeName, string methodName, params object[] parameters) {
            if (this.CurrAssembly == null)
                return default(T);
            var type = this.CurrAssembly.GetType(typeName);
            var obj = Activator.CreateInstance(type);

            return (T)type.GetMethod(methodName).Invoke(obj, parameters);
            //Expression<Func<T>> lambda = Expression.Lambda<Func<T>>(Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
            //return lambda.Compile()();
        }

        public void ExecuteMethod(string typeName, string methodName, params object[] parameters) {
            if (this.CurrAssembly == null)
                return;
            var type = this.CurrAssembly.GetType(typeName);
            var obj = Activator.CreateInstance(type);

            type.GetMethod(methodName).Invoke(obj, parameters);
        }

        public void ExecuteMethod(object instance, string methodName, params object[] parameters) {
            var type = instance.GetType();
            type.GetMethod(methodName).Invoke(instance, parameters);
        }
    }
}
