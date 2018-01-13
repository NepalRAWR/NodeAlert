using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace NodeAlert
{
    public class SaveManager
    {
        ConfigNode CfgNode = new ConfigNode();

        public void Load(string cfgPath)
        {
            CfgNode = ConfigNode.Load(Pathfinder.dllPath() + cfgPath);
        }
        public void Save(string cfgPath)
        { CfgNode.Save(Pathfinder.dllPath() + cfgPath);
        }


        #region get
        public string getStringValue(string NodeName)
        {
            string Value = CfgNode.GetValue(NodeName);
            return Value;
        }
        public int getIntValue(string NodeName)
        {
            int Value =int.Parse(CfgNode.GetValue(NodeName));
            return Value;
        }
        public bool getBoolValue(string NodeName)
        {
            bool Value =bool.Parse(CfgNode.GetValue(NodeName));
            return Value;
        }
        public double getDoubleValue(string NodeName)
        {
            double Value =double.Parse(CfgNode.GetValue(NodeName));
            return Value;
        }
        public float getFloatValue(string NodeName)
        {
            float Value = float.Parse(CfgNode.GetValue(NodeName));
            return Value;
        }
        #endregion
        #region set
        public void setStringValue(string NodeName, string NodeValue)
        { CfgNode.SetValue(NodeName, NodeValue); }
        public void setIntValue(string NodeName, int NodeValue)
        { CfgNode.SetValue(NodeName, NodeValue.ToString()); }
        public void setBoolValue(string NodeName, bool NodeValue)
        { CfgNode.SetValue(NodeName, NodeValue.ToString()); }
        public void setDoubleValue(string NodeName, double NodeValue)
        { CfgNode.SetValue(NodeName, NodeValue.ToString()); }
        public void setFloatValue(string NodeName, float NodeValue)
        { CfgNode.SetValue(NodeName, NodeValue.ToString()); }
        #endregion
    }
}