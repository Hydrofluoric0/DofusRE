using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o
{
    static class GameDataCenter
    {
        private static Dictionary<string, Type> m_classes;

        private static void initGameDataClasses()
        {
            m_classes = new Dictionary<string, Type>();

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var interfaceType = typeof(AbstractGameDataClass);
                var types = asm.GetTypes().Where(t => interfaceType.IsAssignableFrom(t) && t.FullName != interfaceType.FullName).ToList();
                foreach (var type in types)
                {
                    var fields = type.GetFields();
                    var moduleField = fields.Where(f => f.Name == "MODULE").FirstOrDefault();

                    if (moduleField != null)
                    {
                        var moduleName = moduleField.GetRawConstantValue().ToString();
                        m_classes[moduleName.ToString()] = type;
                    }
                    m_classes[type.Name] = type;
                }
            }
        }

        public static Type GetGameDataClassByName(string name)
        {
            if (m_classes == null)
            {
                initGameDataClasses();
            }
            return m_classes[name];
        }
    }
}
