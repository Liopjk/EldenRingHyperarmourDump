// See https://aka.ms/new-console-template for more information

using SoulsFormats;
using SoulsAssetPipeline;


BND4 c000 = BND4.Read(@"../resources/1.07/c0000.anibnd.dcx");
String x = new String("a478.tae");
Console.WriteLine("BinderFiles:");
foreach (BinderFile f in c000.Files)
{
    if (f.Name.EndsWith(x))
    {
        
        Console.WriteLine($"{f.GetType()}: {f.Name} ({f.Bytes.Length} bytes)");
        TAE3 tae = TAE3.Read(f.Bytes);
        
        Console.WriteLine($"Animations: {tae.Animations.Count}");
        
    }
    
}