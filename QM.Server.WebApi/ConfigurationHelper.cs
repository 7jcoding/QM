using System;
using System.Configuration;
using System.IO;

namespace QM.Server.WebApi {
    /// <summary>
    /// 
    /// </summary>
    public abstract class ConfigurationHelper {
        /// <summary>
        /// 獲取自訂配置,如果配置不存在,產生實體一個新對配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSection<T>(string configFile = null) where T : ConfigurationSection, new() {
            if (string.IsNullOrWhiteSpace(configFile))
                configFile = typeof(T).Name;

            var section = (T)ConfigurationManager.GetSection(configFile);
            if (section == null) {
                section = new T();

                var file = string.Format(@"{0}\{1}.config", AppDomain.CurrentDomain.BaseDirectory, configFile);
                if (File.Exists(file)) {
                    var raw = File.ReadAllText(file);
                    section.SectionInformation.ConfigSource = string.Format("{0}.config", configFile);
                    section.SectionInformation.SetRawXml(raw);

                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.Sections.Add(typeof(T).Name, section);
                }
            }

            return section;
        }
    }
}
