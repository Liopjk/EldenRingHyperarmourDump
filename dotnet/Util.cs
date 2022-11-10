using SoulsFormats;
using SoulsAssetPipeline.Animation;
using System.Xml;

namespace EldenRingHyperArmourDump
{
    class Util
    {
        public static void DumpAnimData(string outFilePath = "animationToughnessParams.csv", string resPath = @"../resources/")
        {
            if (!resPath.EndsWith("/"))
            {
                resPath+= "/";
            }
            using StreamWriter taeFile = new StreamWriter(outFilePath);
            {
                BND4 c000 = BND4.Read(resPath + "c0000.anibnd.dcx");
                taeFile.WriteLine("TaeId,AnimId,FullAnimId,ToughnessParamId,ToughnessType");
                foreach (BinderFile f in c000.Files)
                {
                    if (f.Name.EndsWith(".tae")) 
                    {
                        TAE tae = TAE.Read(f.Bytes);
                        foreach (var a in tae.Animations)
                        {                
                            foreach (var e in a.Events)
                            {
                                if (e.Type == 795) // InvokeDS3Poise
                                {
                                    byte[] parameters = e.GetParameterBytes(false);
                                    int ToughnessParamId = parameters[0];
                                    ushort ToughnessType = parameters[1];
                                    string paramsString = BitConverter.ToString(parameters);
                                    int taeId = f.ID;
                                    if (taeId >= 5000000)
                                    {
                                        taeId -= 5000000;
                                    }
                                    string displayAnimId = a.ID.ToString().PadLeft(6,'0');
                                    string displayTaeId = taeId.ToString().PadLeft(3,'0');
                                    taeFile.WriteLine($"a{displayTaeId},{displayAnimId},a{displayTaeId}_{displayAnimId},{ToughnessParamId},{ToughnessType}");
                                }
                            }
                        }
                    }
                }
            }
        }
    
        public static void DumpToughnessParamData(  string outFilePath="toughnessParams.csv",
                                                    string resPath=@"../resources/")
        {
            using StreamWriter toughFile = new StreamWriter(outFilePath);
            {
                
                if (!resPath.EndsWith("/"))
                {
                    resPath+= "/";
                }
                PARAMDEF toughParamDef = PARAMDEF.XmlDeserialize(resPath + "ToughnessParam.xml");
                BND4 gameParams = BND4.Read(resPath + "gameparam.parambnd.dcx");
                foreach (var f in gameParams.Files)
                {
                    if (f.Name.EndsWith("ToughnessParam.param"))
                    {
                        
                        PARAM toughnessParam = PARAM.Read(f.Bytes);
                        bool success = toughnessParam.ApplyParamdefCarefully(toughParamDef);
                        toughFile.WriteLine($"toughnessParamId,correctionRate,minToughness,isNonEffectiveCorrectionForMin,spEffectId,proCorrectionRate");
                        foreach (var r in toughnessParam.Rows)
                        {
                            int toughnessParamId = r.ID;
                            float correctionRate = (float)r.Cells[0].Value;
                            UInt16 minToughness = (UInt16)r.Cells[1].Value;
                            
                            UInt16 isNonEffectiveCorrectionForMin = (UInt16)(byte)r.Cells[2].Value;
                            Int32 spEffectId = (Int32)r.Cells[4].Value;
                            float proCorrectionRate = (float)r.Cells[5].Value;
                            toughFile.WriteLine($"{toughnessParamId},{correctionRate},{minToughness},{isNonEffectiveCorrectionForMin},{spEffectId},{proCorrectionRate}");
                        }
                    }
                }
            }
        }




    }

    
}

