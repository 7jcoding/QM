using System.Configuration;

namespace QM.Server.WebApi {
    public class UserConfig : ConfigurationSection {

        #region 配置節設置，設定檔中有不能識別的元素、屬性時，使其不報錯
        /// <summary>
        /// 遇到未知屬性時，不報錯
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value) {
            //return base.OnDeserializeUnrecognizedAttribute(name, value);
            return true;
        }

        /// <summary>
        /// 遇到未知元素時，不報錯
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader) {
            //return base.OnDeserializeUnrecognizedElement(elementName, reader);
            return true;
        }

        #endregion

        /// <summary>
        /// 系统代码
        /// </summary>
        [ConfigurationProperty("user", IsRequired = true)]
        public string User {
            get {
                return (string)this["user"];
            }
            set {
                this["user"] = value;
            }
        }

        [ConfigurationProperty("password")]
        public string Pwd {
            get {
                return this["password"].ToString();
            }
            set {
                this["password"] = value;
            }
        }

    }
}
