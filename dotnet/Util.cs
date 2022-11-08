using SoulsFormats;
using SoulsAssetPipeline.Animation;

namespace EldenRingHyperArmourDump
{
    class Util
    {
        public static void DumpAnimData(string outFilePath = "all_toughnessParams.csv", string anibndPath = @"../resources/1.07/c0000.anibnd.dcx")
        {
            using StreamWriter taeFile = new StreamWriter(outFilePath);
            {
                BND4 c000 = BND4.Read(anibndPath);
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
    }


}

