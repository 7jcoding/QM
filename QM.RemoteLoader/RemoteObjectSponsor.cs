using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

/*
 * 远程对象的租约默认为5分钟, 超过5分钟后,如果没有续约,就会抛出如下异常
 * System.Runtime.Remoting.RemotingException: 对象“/6cfd8ad9_db67_48e8_88e1_66027c9a3b93/8sdrl8yytk1yxgrcduynew5d_2.rem”
 * 已断开连接或不在该服务器上。 
 * 
 * http://www.cnblogs.com/WangJinYang/archive/2013/02/05/2892911.html
 * http://www.cnblogs.com/luomingui/archive/2011/07/09/2101779.html
*/

namespace QM.RemoteLoader {

    /// <summary>
    /// 远程对象续约
    /// </summary>
    public class RemoteObjectSponsor : MarshalByRefObject, ISponsor {
        public TimeSpan Renewal(ILease lease) {
#if DEBUG
            Console.WriteLine("续约");
#endif
            return TimeSpan.FromMinutes(5);
        }
    }
}
