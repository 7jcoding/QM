using System;

namespace QM.Server.ApiClient {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class ParseException : Exception {

        public byte[] TargetData { get; set; }

        public Type TargetType { get; set; }
    }
}
