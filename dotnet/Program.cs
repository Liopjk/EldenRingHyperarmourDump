// See https://aka.ms/new-console-template for more information

using SoulsFormats;
using SoulsAssetPipeline;
using SoulsAssetPipeline.Animation;



BND4 c000 = BND4.Read(@"../resources/1.07/c0000.anibnd.dcx");

Console.WriteLine("TaeId,AnimId,ToughnessParamId,ToughnessType");
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
                    Console.WriteLine($"{f.ID},{a.ID},{ToughnessParamId},{ToughnessType}");
                }
                
            }


        }
        
    }
    
}